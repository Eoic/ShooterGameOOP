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
import com.badlogic.serializables.SerializablePlayer;
import com.badlogic.util.*;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.concurrent.*;

public class Loop implements Observer {
    // Game loop
    private ExecutorService messageExecutor = Executors.newSingleThreadExecutor();
    private static final long timeStep = 1000 / Constants.FPS;
    private ScheduledExecutorService executor;
    private MessageEmitter messageEmitter;
    private boolean isRunning = false;
    private JsonParser jsonParser;
    private GameRoom gameRoom;
    private long lastTime;
    private long delta = 0;

    // Game entities
    private ArrayList<Bonus> bonuses;
    private GameManager gameManager;
    private Player player;
    private Map map;

    private RemotePlayer remotePlayer;

    // Starts game loop
    public void start() {
        if (isRunning)
            return;

        isRunning = true;
        initialize();
        executor = Executors.newSingleThreadScheduledExecutor();
        lastTime = System.currentTimeMillis();
        executor.scheduleAtFixedRate(() -> {
            update();
            render();
        }, 0, timeStep, TimeUnit.MILLISECONDS);
    }

    // Stops game loop
    public void stop() {
        if (!isRunning)
            return;

        isRunning = false;
        executor.shutdown();
    }

    // Initializes game resources
    private void initialize() {
        Assets.load();
        gameRoom = new GameRoom();
        messageEmitter = new MessageEmitter();
        messageEmitter.addListener(this);
        gameManager = new GameManager();
        map = new Map(Constants.MAP_WIDTH, Constants.MAP_HEIGHT, gameManager);
        jsonParser = new JsonParser();
        player = new Player(gameManager, messageEmitter);
        bonuses = new ArrayList<>();
        remotePlayer = new RemotePlayer(gameManager.getWindow(), gameManager.getCamera(), Assets.getSprite("enemy"));

        // Set UI events
        gameManager.getWindow().setCreateGameBtnEvent(actionEvent -> {
            var message = new Message(RequestCode.CreateGame, "Create game pls.");
            messageEmitter.send(jsonParser.serialize(message));
        });

        gameManager.getWindow().setQuitGameBtnEvent(actionEvent -> {
            var message = new Message(RequestCode.QuitGame, "Exiting.");
            messageEmitter.send(jsonParser.serialize(message));
        });

        gameManager.getWindow().setJoinGameBtnEvent(actionEvent -> {
            var message = new Message(RequestCode.JoinGame, "Joining game.");
            messageEmitter.send(jsonParser.serialize(message));
        });
    }

    // Updates game entities (e.g. position)
    private void update() {
        gameManager.getInputManager().tick();
        player.update((int)delta);
        // remotePlayer.update((int)delta);
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
        map.render(graphics); // TODO: Generate map
        // bonuses.forEach(bonus -> bonus.render(graphics));
        // remotePlayer.render(graphics);
        gameRoom.getPlayers().forEach(player -> player.render(graphics));
        // Stop rendering

        bufferStrategy.show();
        graphics.dispose();
        lastTime = currentTime;
    }

    // Handle messages received from server.
    // TODO: Split message handling to separate methods
    @Override
    public void update(Object data) {
        var message = jsonParser.deserialize(data.toString(), Message.class);

        if (message.getType() == ResponseCode.GameCreated) {
            var position = jsonParser.deserialize(message.getPayload(), Point.class);
            player = new Player(gameManager, messageEmitter);
            player.getPosition().set(new Vector(position.getX(), position.getY()));
            // gameManager.getCamera().getOffset().set(new Vector(position.getX(), position.getY()));
            gameRoom.addPlayer(player);
        } else if (message.getType() == ResponseCode.PositionUpdated) {
            messageExecutor.submit(() -> {
                var players = jsonParser.deserializeList(message.getPayload(), SerializablePlayer.class);
                players.forEach(serializablePlayer -> {
                    if (serializablePlayer.getType() == 10) {
                        var position = serializablePlayer.getPosition();
                        gameRoom.getPlayers().get(0).position.set(new Vector(position.getX(), position.getY()));
                    } else {
                        // var p = serializablePlayer.getPosition();
                        // remotePlayer.position = new Vector(p.getX(), p.getY());
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
            gameRoom.getPlayers().remove(0); // For now....
        } else if (message.getType() == ResponseCode.GameJoined) {
            // Temporary
            System.out.println("Game joined");
            var position = jsonParser.deserialize(message.getPayload(), Point.class);
            player = new Player(gameManager, messageEmitter);
            player.getPosition().set(new Vector(position.getX(), position.getY()));
            gameRoom.addPlayer(player);
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
}
