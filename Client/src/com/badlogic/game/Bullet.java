package com.badlogic.game;

import com.badlogic.core.GameObject;
import com.badlogic.gfx.Assets;
import com.badlogic.gfx.Camera;
import com.badlogic.util.Constants;
import com.badlogic.util.SpriteKeys;
import com.badlogic.util.Vector;

import java.awt.*;

public class Bullet extends GameObject {
    private boolean isActive = false;
    private Vector direction;
    private Vector origin;
    private Window window;
    private Camera camera;
    private int speed;

    public Bullet(int speed, Window window, Camera camera, String bulletType) {
        this.sprite = Assets.getSprite(bulletType);
        this.origin = new Vector();
        this.position = new Vector();
        this.window = window;
        this.camera = camera;
        this.speed = speed;
    }

    @Override
    public void render(Graphics graphics) {
        if (isActive) {
            var posX = (int) (this.position.getX() - camera.getOffset().getX()) - Constants.SPRITE_WIDTH_HALF;
            var posY = (int) (this.position.getY() - camera.getOffset().getY()) - Constants.SPRITE_HEIGHT_HALF;
            graphics.drawImage(this.sprite, posX, posY, null);
        }
    }

    @Override
    public void update(int delta) {
        if (isActive) {
            var change = direction.multiply(speed);
            this.position.add(change);
        }
    }

    public void launch(Vector target, Vector globalOrigin) {
        this.origin = new Vector(window.getSize().width / 2.0, window.getSize().height / 2.0);
        this.position = new Vector(globalOrigin.getX(), globalOrigin.getY());
        this.direction = target.difference(origin).getNormalized();
        this.isActive = true;
    }

    public boolean getActiveState() {
        return this.isActive;
    }

    public void dispose() {
        this.direction = new Vector();
        this.isActive = false;
    }
}
