package com.badlogic.game;

import com.badlogic.core.Entity;
import com.badlogic.gfx.Assets;
import com.badlogic.input.InputManager;
import com.badlogic.util.Vector;

import java.awt.*;

public class Player implements Entity {
    private InputManager inputManager;

    public Player(InputManager inputManager) {
        this.inputManager = inputManager;
    }

    @Override
    public Vector getPosition() {
        return null;
    }

    @Override
    public void render(Graphics graphics) {
        graphics.drawImage(Assets.getSprite("player"), position.getX(), position.getY(), null);
    }

    @Override
    public void update(int delta) {
        if (inputManager.left)
            position.add(-1 * delta, 0);
        if (inputManager.right)
            position.add(1 * delta, 0);
        if (inputManager.up)
            position.add(0, -1 * delta);
        if (inputManager.down)
            position.add(0, 1 * delta);
    }
}
