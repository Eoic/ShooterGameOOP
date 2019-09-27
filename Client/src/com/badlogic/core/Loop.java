package com.badlogic.core;

import com.badlogic.game.Player;
import com.badlogic.gfx.Assets;
import com.badlogic.gfx.Window;
import com.badlogic.input.InputManager;
import com.badlogic.util.Constants;

import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

public class Loop {
    private static final long timeStep = 1000 / Constants.FPS;
    private InputManager inputManager = new InputManager();
    private ScheduledExecutorService executor;
    private boolean isRunning = false;
    private Window window;
    private long lastTime;
    private long delta = 0;

    // Game entities
    private Player player = new Player(inputManager);

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

    public void stop() {
        if (!isRunning)
            return;

        isRunning = false;
        executor.shutdown();
    }

    private void initialize() {
        Assets.load();
        window = new Window(Constants.WIDTH, Constants.HEIGHT, Constants.TITLE, Constants.IS_RESIZEABLE);
        window.addKeyListener(inputManager);
        window.getCanvas().addMouseListener(inputManager);
    }

    private void update() {
        inputManager.tick();
        player.update((int)delta);
    }

    private void render() {
        var bufferStrategy = window.getCanvas().getBufferStrategy();
        long currentTime = System.currentTimeMillis();
        delta = (currentTime - lastTime);

        if (bufferStrategy == null) {
            window.getCanvas().createBufferStrategy(Constants.BUFFER_COUNT);
            return;
        }

        var graphics = bufferStrategy.getDrawGraphics();
        graphics.clearRect(0, 0, window.getWidth(), window.getHeight());
        // Start rendering
        player.render(graphics);
        // Stop rendering
        bufferStrategy.show();
        graphics.dispose();
        lastTime = currentTime;
    }
}
