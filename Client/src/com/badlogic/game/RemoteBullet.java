package com.badlogic.game;

import com.badlogic.core.GameObject;
import com.badlogic.gfx.Assets;
import com.badlogic.gfx.Camera;
import com.badlogic.util.Constants;
import com.badlogic.util.SpriteKeys;
import com.badlogic.util.Vector;

import java.awt.*;

public class RemoteBullet extends GameObject {
    private Vector direction = new Vector(0, 0);

    public RemoteBullet(Vector position, Vector direction) {
        this.position.set(position);
        this.direction.set(direction);
        this.sprite = Assets.getSprite(SpriteKeys.BULLET_TYPE_ONE);
    }

    @Override
    public void render(Graphics graphics) {

    }

    public void render(Graphics graphics, Camera camera) {
        int posX = (int)position.getX() - (int)camera.getOffset().getX() - Constants.SPRITE_WIDTH_HALF;
        int posY = (int)position.getY() - (int)camera.getOffset().getY() - Constants.SPRITE_WIDTH_HALF;
        graphics.drawImage(this.sprite, posX, posY, null);
    }

    // NOTE: Checking for constraints is not necessary
    @Override
    public void update(int delta) {
        var change = direction.multiply(delta * Constants.DEFAULT_BULLET_SPEED);
        this.position.add(change);
    }

    public void setDirection(Vector direction) {
        this.direction.set(direction);
    }

    public void setPosition(Vector position) {
        this.position.set(position);
    }
}
