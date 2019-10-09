package com.badlogic.gfx;

import com.badlogic.game.GameManager;
import com.badlogic.util.Constants;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Map {
    private int width;
    private int height;
    private GameManager gameManager;

    public Map(int width, int height, GameManager gameManager) {
        this.width = width;
        this.height = height;
        this.gameManager = gameManager;
    }

    public void render(BufferedImage tile, Graphics graphics) {
        for (int i = 0; i < this.width; i++) {
            for (int j = 0; j < height; j++) {
                var camOffset = gameManager.getCamera().getOffset();
                int posX = (i * Constants.SPRITE_WIDTH - (int) camOffset.getX()) - 64;
                int posY = (j * Constants.SPRITE_HEIGHT - (int) camOffset.getY()) - 64;
                graphics.drawImage(tile, posX, posY, null);
            }
        }
    }

    private void loadFromTemplate(String type) {

    }
}
