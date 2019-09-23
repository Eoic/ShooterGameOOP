package com.badlogic.core;

import com.badlogic.gfx.Sprite;
import com.badlogic.gfx.SpriteSheet;
import com.badlogic.gfx.Window;
import com.badlogic.util.AssetsLoader;
import com.badlogic.util.Constants;
import com.badlogic.util.ImageLoader;

import java.awt.*;
import java.util.ArrayList;
import java.util.Date;
import java.util.concurrent.Executors;
import java.util.concurrent.ScheduledExecutorService;
import java.util.concurrent.TimeUnit;

public class Loop {
    private static final long timeStep = 1000 / Constants.FPS;
    private ScheduledExecutorService executor;
    private boolean isRunning = false;
    private ArrayList<Sprite> sprites; // temporary.
    private Window window;
    private long lastTime;

    private int ii = 0;

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
        window = new Window(Constants.WIDTH, Constants.HEIGHT, Constants.TITLE, Constants.IS_RESIZEABLE);
        var spriteSheet = new SpriteSheet(ImageLoader.loadImage(Constants.TEXTURES + "/" + Constants.SPRITE_SHEET),
                                          Constants.SHEET_ROWS, Constants.SHEET_COLUMNS);
        sprites = AssetsLoader.load(spriteSheet, Constants.SPRITE_SHEET_INFO, Constants.SPRITE_WIDTH, Constants.SPRITE_HEIGHT);
    }

    private void update() {
        // Update entities before rendering step
    }

    private void render() {
        var bufferStrategy = window.getCanvas().getBufferStrategy();
        long currentTime = System.currentTimeMillis();
        long delta = (currentTime - lastTime);

        if (bufferStrategy == null) {
            window.getCanvas().createBufferStrategy(Constants.BUFFER_COUNT);
            return;
        }

        var graphics = bufferStrategy.getDrawGraphics();
        graphics.clearRect(0, 0, window.getWidth(), window.getHeight());
        // Start rendering
        System.out.println(delta);
        ii += 0.2 * delta;
        graphics.drawImage(sprites.get(0).getTexture(), 128 + ii, 0, null);
        // Stop rendering
        bufferStrategy.show();
        graphics.dispose();
        lastTime = currentTime;
    }
}
