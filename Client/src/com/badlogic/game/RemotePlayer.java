package com.badlogic.game;

import com.badlogic.core.GameObject;
import com.badlogic.gfx.Assets;
import com.badlogic.gfx.Camera;
import com.badlogic.util.Constants;
import com.badlogic.util.SpriteKeys;
import com.badlogic.util.Vector;

import java.awt.*;
import java.awt.image.BufferedImage;

public class RemotePlayer extends GameObject {
    private Window window;
    private Camera camera;
    private Vector direction;
    private int speed;

    public RemotePlayer(Window window, Camera camera, boolean isFriendly) {
        this.window = window;
        this.camera = camera;
        this.direction = new Vector(0, 0);
        this.speed = Constants.DEFAULT_PLAYER_SPEED;

        if (!isFriendly) {
            this.sprite = Assets.getSprite(SpriteKeys.ENEMY_PLAYER);
        } else {
            this.sprite = Assets.getSprite(SpriteKeys.FRIENDLY_PLAYER);
        }
    }

    @Override
    public void render(Graphics graphics) {
        int posX = (int)position.getX() - (int)camera.getOffset().getX() - Constants.SPRITE_WIDTH_HALF;
        int posY = (int)position.getY() - (int)camera.getOffset().getY() - Constants.SPRITE_WIDTH_HALF;
        graphics.drawImage(this.sprite, posX, posY, null);
    }

    @Override
    public void update(int delta) {
        var change = direction.multiply(delta * speed);
        var newPos = change.sum(this.position);

        if (newPos.getX() >= 0 && newPos.getX() < Constants.MAP_PIXEL_WIDTH - Constants.MAP_TILE_SIZE &&
            newPos.getY() >= 0 && newPos.getY() < Constants.MAP_PIXEL_HEIGHT - Constants.MAP_TILE_SIZE) {
            this.position.add(change);
        }
    }

    public void setDirection(Vector direction) {
        this.direction = direction;
    }

    public void setPosition(Vector position) {
        this.position = position;
    }
}
