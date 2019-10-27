package com.badlogic.core;

import com.badlogic.core.factory.Bonus;
import com.badlogic.core.factory.BonusFactory;
import com.badlogic.core.factory.BonusType;
import com.badlogic.core.observer.Observer;
import com.badlogic.game.GameManager;
import com.badlogic.game.Player;
import com.badlogic.game.RemotePlayer;
import com.badlogic.gfx.Assets;
import com.badlogic.gfx.Map;
import com.badlogic.network.GameRoom;
import com.badlogic.network.Message;
import com.badlogic.network.MessageEmitter;
import com.badlogic.network.RequestCode;
import com.badlogic.network.ResponseCode;
import com.badlogic.serializables.SerializableBonus;
import com.badlogic.serializables.SerializableGame;
import com.badlogic.serializables.SerializablePlayer;
import com.badlogic.util.*;
import com.badlogic.util.Point;
import com.badlogic.util.Vector;

import java.awt.*;
import java.util.*;
import java.util.List;
import java.util.concurrent.*;

public class Loop implements Observer {
    // Status
    private boolean clientInGame = false;

    // Game loop
    private static final long timeStep = 1000 / Constants.FPS;
    private ScheduledExecutorService executor;
    private ExecutorService messageExecutor;
    private MessageEmitter messageEmitter;
    private JsonParser jsonParser;
    private boolean isRunning;
    private GameRoom gameRoom;
    private long delta = 0;
    private long lastTime;

    // Game entities
    private HashMap<String, RemotePlayer> roomPlayers;
    private ArrayList<Bonus> bonuses;
    private GameManager gameManager;
    private Player player;
    private Map map;

    public Loop() {
        messageExecutor = Executors.newSingleThreadExecutor();
        messageEmitter = new MessageEmitter();
        executor = Executors.newSingleThreadScheduledExecutor();
        jsonParser = new JsonParser();
        gameRoom = new GameRoom();
        gameManager = new GameManager();
        map = new Map(Constants.MAP_WIDTH, Constants.MAP_HEIGHT, gameManager);
        player = new Player(gameManager, messageEmitter);
        bonuses = new ArrayList<>();
        roomPlayers = new HashMap<>();
        this.initialize();
    }

    // Starts game loop
    public void start() {
        if (isRunning)
            return;

        isRunning = true;
        lastTime = System.currentTimeMillis();
        executor.scheduleAtFixedRate(() -> {
            update();
            render();
        }, 0, timeStep, TimeUnit.MILLISECONDS);
    }

    // Initializes game resources
    private void initialize() {
        // Check if communication with server is possible.
        if (messageEmitter.isConnectionFailed())
            return;

        // Set event listener
        messageEmitter.addListener(this);

        // Load graphics.
        Assets.load();

        // Set UI events
        // # Create game
        gameManager.getWindow().setCreateGameBtnEvent(actionEvent -> {
            var message = new Message(RequestCode.CreateGame, "");
            messageEmitter.send(jsonParser.serialize(message));
        });

        // # Exit game
        gameManager.getWindow().setQuitGameBtnEvent(actionEvent -> {
            var message = new Message(RequestCode.QuitGame, "Exiting.");
            messageEmitter.send(jsonParser.serialize(message));
        });

        gameManager.getWindow().setRefreshGameListBtnEvent(actionEvent -> {
            messageEmitter.send(jsonParser.serialize(new Message(RequestCode.FormGameList, "")));
        });

        /*
        gameManager.getWindow().setJoinGameBtnEvent(actionEvent -> {
            var message = new Message(RequestCode.JoinGame, "Joining game.");
            messageEmitter.send(jsonParser.serialize(message));
        });
        */

        // Load game list
        messageEmitter.send(jsonParser.serialize(new Message(RequestCode.FormGameList, "")));
    }

    // Updates game entities (e.g. position)
    private void update() {
        gameManager.getInputManager().tick();
        roomPlayers.forEach((key, value) -> value.update((int) delta));
        player.update((int)delta);
    }

    // Renders game objects
    private void render() {
        var bufferStrategy = gameManager.getWindow().getCanvas().getBufferStrategy();
        long currentTime = System.currentTimeMillis();
        delta = (currentTime - lastTime);

        if (bufferStrategy == null) {
            gameManager.getWindow().getCanvas().createBufferStrategy(Constants.BUFFER_COUNT);
            return;
        }

        var graphics = bufferStrategy.getDrawGraphics();
        graphics.clearRect(0, 0, gameManager.getWindow().getWidth(), gameManager.getWindow().getHeight());

        // Start rendering
        if (clientInGame) {
            map.render(graphics);
            player.render(graphics);
            roomPlayers.forEach((key, value) -> value.render(graphics));
            // bonuses.forEach(bonus -> bonus.render(graphics));
            // gameRoom.getPlayers().forEach(player -> player.render(graphics));
        }
        // Stop rendering

        bufferStrategy.show();
        graphics.dispose();
        lastTime = currentTime;
    }

    // Handle messages received from server.
    // TODO: Split message handling to separate methods
    @Override
    public void update(Object data) {
        // Deserialize received message.
        var message = jsonParser.deserialize(data.toString(), Message.class);

        // Take action according to event type.
        // # List all available games in the UI
        if (message.getType() == ResponseCode.GameListFormed) {
            List<SerializableGame> gameList = jsonParser.deserializeList(message.getPayload(), SerializableGame.class);
            gameManager.getWindow().updateGameList(new ArrayList<>(gameList), messageEmitter);
        }
        // # Create and place player on the map.
        else if (message.getType() == ResponseCode.GameCreated) {
            var position = jsonParser.deserialize(message.getPayload(), Point.class);
            player = new Player(gameManager, messageEmitter);
            player.getPosition().set(new Vector(position.getX(), position.getY()));
            gameRoom.addPlayer(player);
            this.setActiveGameMode();
        }
        // # Update positions af all players in the room.
        else if (message.getType() == ResponseCode.PositionUpdated) {
            messageExecutor.submit(() -> {
                var players = jsonParser.deserializeList(message.getPayload(), SerializablePlayer.class);

                players.forEach(serializablePlayer -> {
                    // Update this player.
                    if (serializablePlayer.getType() == 10) {
                        var position = serializablePlayer.getPosition();
                        gameRoom.getPlayers().get(0).position.set(new Vector(position.getX(), position.getY()));
                    }
                    // Update other player in the room.
                    else {
                        // :(
                    }
                });
            });
        } else if (message.getType() == ResponseCode.BonusesCreated) {
            messageExecutor.submit(() -> {
                var serializableBonuses = jsonParser.deserializeList(message.getPayload(), SerializableBonus.class);
                createBonuses(serializableBonuses);
            });
        } else if (message.getType() == ResponseCode.GameQuit) {
            System.out.println(message.getPayload());
            gameRoom.getPlayers().remove(0);
        } else if (message.getType() == ResponseCode.GameJoined) {
            var position = jsonParser.deserialize(message.getPayload(), Point.class);
            player = new Player(gameManager, messageEmitter);
            player.getPosition().set(new Vector(position.getX(), position.getY()));
            gameRoom.addPlayer(player);
            this.setActiveGameMode();
        } else if (message.getType() == ResponseCode.NewPlayerJoined) {
            System.out.println("new player joined this room");
        }
    }

    private void createBonuses(List<SerializableBonus> serializableBonuses) {
        serializableBonuses.forEach(serializableBonus -> {
            Bonus bonus = null;

            if (Objects.equals(serializableBonus.getType(), BonusType.HEALTH))
                bonus = BonusFactory.create(BonusType.HEALTH, gameManager);
            else if (serializableBonus.getType().equals(BonusType.AMMO))
                bonus = BonusFactory.create(BonusType.AMMO, gameManager);
            else if (Objects.equals(serializableBonus.getType(), BonusType.SPEED))
                bonus = BonusFactory.create(BonusType.SPEED, gameManager);

            if (bonus != null) {
                bonus.setBonusAmount(serializableBonus.getBonusAmount());
                bonus.setLifespan(serializableBonus.getLifespan());
                bonus.position.set(new Vector(serializableBonus.getPosition().getX(), serializableBonus.getPosition().getY()));
                bonuses.add(bonus);
            }
        });
    }

    private void setActiveGameMode() {
        clientInGame = true;
        gameManager.getWindow().getClientHealthBar().setVisible(true);
        gameManager.getWindow().setCanvasColor(new Color(64, 67, 78));
        this.gameManager.getWindow().setActiveGameMode();
    }
}
