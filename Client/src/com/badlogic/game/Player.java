package com.badlogic.game;

import com.badlogic.core.Entity;
import com.badlogic.gfx.Assets;
import com.badlogic.util.Constants;
import com.badlogic.util.Vector;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Player implements Entity {
    private GameManager gameManager;
    private BufferedImage sprite;

    public Player(GameManager gameManager) {
        this.gameManager = gameManager;
        this.sprite = Assets.getSprite("player");
    }

    @Override
    public Vector getPosition() {
        return position;
    }

    @Override
    public void render(Graphics graphics) {
        var camOffset = gameManager.getCamera().getOffset();
        var windowSize = gameManager.getWindow().getSize();
        int posX = ((int)position.getX() - (int)camOffset.getX()) + (windowSize.width / 2 - Constants.SPRITE_WIDTH / 2);
        int posY = ((int)position.getY() - (int)camOffset.getY()) + (windowSize.height / 2 - Constants.SPRITE_HEIGHT / 2);
        graphics.drawImage(sprite, posX, posY, null);
    }

    @Override
    public void update(int delta) {
        if (gameManager.getInputManager().left) {
            gameManager.getCamera().getOffset().add(-1, 0);
            position.add(-delta, 0);
        }
        if (gameManager.getInputManager().right) {
            gameManager.getCamera().getOffset().add(1, 0);
            position.add(delta, 0);
        }
        if (gameManager.getInputManager().up) {
            gameManager.getCamera().getOffset().add(0, -1);
            position.add(0, -delta);
        }
        if (gameManager.getInputManager().down) {
            gameManager.getCamera().getOffset().add(0, 1);
            position.add(0, delta);
        }
    }
}
