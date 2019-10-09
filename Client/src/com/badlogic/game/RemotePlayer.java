package com.badlogic.game;

import com.badlogic.core.GameObject;
import com.badlogic.gfx.Camera;
import com.badlogic.util.Constants;
import com.badlogic.util.Vector;

import java.awt.*;
import java.awt.image.BufferedImage;

public class RemotePlayer extends GameObject {
    private Window window;
    private Camera camera;
    private Vector direction;

    public RemotePlayer(Window window, Camera camera, BufferedImage sprite) {
        this.window = window;
        this.camera = camera;
        this.sprite = sprite;
        this.direction = new Vector(0, 0);
    }

    @Override
    public void render(Graphics graphics) {
        var windowSize = window.getSize();
        int posX = Constants.SPRITE_WIDTH - (int)camera.getOffset().getX() + (windowSize.width / 2 - Constants.SPRITE_WIDTH - 64 + (int)this.position.getX());
        int posY = Constants.SPRITE_HEIGHT - (int)camera.getOffset().getY() + (windowSize.height / 2 - Constants.SPRITE_WIDTH - 64 + (int)this.position.getY());
        graphics.drawImage(this.sprite, posX, posY, null);
    }

    @Override
    public void update(int delta) {
        this.position.add(direction);
    }
}
