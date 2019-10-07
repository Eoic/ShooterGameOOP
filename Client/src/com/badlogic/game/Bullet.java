package com.badlogic.game;

import com.badlogic.core.GameObject;
import com.badlogic.gfx.Assets;
import com.badlogic.gfx.Camera;
import com.badlogic.util.Constants;
import com.badlogic.util.Vector;

import java.awt.*;

public class Bullet extends GameObject {
    private boolean isActive = false;
    private Vector direction;
    private Vector origin;
    private Window window;
    private Vector change;
    private Camera camera;
    private int step;
    private int speed;

    public Bullet(int speed, Window window, Camera camera) {
        this.window = window;
        this.speed = speed;
        this.origin = new Vector(0, 0);
        this.change = new Vector(0, 0);
        this.camera = camera;
    }

    @Override
    public void render(Graphics graphics) {
        if (isActive) {
            graphics.drawImage(Assets.getSprite("bulletVarOne"), ((int)change.getX() - 64), ((int)change.getY() - 64), null);
        }
    }

    @Override
    public void update(int delta) {
        if (isActive) {
            this.change = origin.sum(direction.multiply(step * speed));
            step++;
        }
    }

    public void launch(Vector target) {
        var windowSize = window.getSize();
        this.origin = new Vector(windowSize.width / 2.0, windowSize.height / 2.0);
        this.isActive =  true;
        this.direction = target.difference(origin).getNormalized();
    }

    public boolean getActiveState() {
        return this.isActive;
    }

    public void dispose() {
        this.isActive = false;
        this.direction = new Vector(0, 0);
        this.step = 0;
    }
}
