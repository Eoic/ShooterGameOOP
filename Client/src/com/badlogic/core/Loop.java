package com.badlogic.core;

import com.badlogic.core.observer.Observer;
import com.badlogic.game.GameManager;
import com.badlogic.game.Player;
import com.badlogic.gfx.Assets;
import com.badlogic.gfx.Map;
import com.badlogic.network.MessageEmitter;
import com.badlogic.util.Constants;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

public class Loop implements Observer {
    // Game loop
    private static final long timeStep = 1000 / Constants.FPS;
    private ScheduledExecutorService executor;
    private MessageEmitter messageEmitter;
    private boolean isRunning = false;
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

        messageEmitter.send("Hello there");
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
        gameManager = new GameManager();
        player = new Player(gameManager);
        messageEmitter = new MessageEmitter();
        map = new Map(10, 10, gameManager);
        messageEmitter.addListener(this);
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
        player.render(graphics);
        // Stop rendering
        bufferStrategy.show();
        graphics.dispose();
        lastTime = currentTime;
    }

    // Received from server.
    @Override
    public void update(Object data) {
        System.out.println("Received: " + data);
    }
}
