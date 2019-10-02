package com.badlogic.core;

import com.badlogic.core.observer.Observer;
import com.badlogic.game.GameManager;
import com.badlogic.game.Player;
import com.badlogic.gfx.Assets;
import com.badlogic.gfx.Map;
import com.badlogic.network.GameRoom;
import com.badlogic.network.Message;
import com.badlogic.network.MessageEmitter;
import com.badlogic.network.MessageType;
import com.badlogic.util.Constants;
import com.badlogic.util.JsonParser;
import com.badlogic.util.Point;
import com.badlogic.util.Vector;

import java.util.ArrayList;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

public class Loop implements Observer {
    // Game loop
    private static final long timeStep = 1000 / Constants.FPS;
    private ScheduledExecutorService executor;
    private MessageEmitter messageEmitter;
    private boolean isRunning = false;
    private JsonParser jsonParser;
    private GameRoom gameRoom;
    private long lastTime;
    private long delta = 0;

    // Game entities
    private GameManager gameManager;
    private Player player;
    private Map map;

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
        map = new Map(10, 10, gameManager);
        jsonParser = new JsonParser();
        player = new Player(gameManager, messageEmitter);

        // Set UI events
        gameManager.getWindow().setCreateGameBtnEvent(actionEvent -> {
            var message = new Message(MessageType.CreateGame, "Create game pls.");
            System.out.println(jsonParser.serialize(message));
            messageEmitter.send(jsonParser.serialize(message));
        });
    }

    // Updates game entities (e.g. position)
    private void update() {
        gameManager.getInputManager().tick();
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
        map.render(Assets.getSprite("normalTile"), graphics);
        gameRoom.getPlayers().forEach(player -> player.render(graphics));
        // Stop rendering
        bufferStrategy.show();
        graphics.dispose();
        lastTime = currentTime;
    }

    // Handle messages received from server.
    @Override
    public void update(Object data) {
        var message = jsonParser.deserialize(data.toString(), Message.class);

        if (message.getType() == MessageType.GameCreated) {
            var position = jsonParser.deserialize(message.getPayload(), Point.class);
            var player = new Player(gameManager, messageEmitter);
            player.position.set(new Vector(position.getX(), position.getY()));
            gameRoom.addPlayer(player);
        } else if (message.getType() == MessageType.PositionUpdate) {
            var position = jsonParser.deserialize(message.getPayload(), Point.class);
            gameRoom.getPlayers().get(0).position.set(new Vector(position.getX(), position.getY()));
        }
    }
}
