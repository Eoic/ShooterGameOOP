package com.badlogic.gfx;

import com.badlogic.game.GameManager;
import com.badlogic.util.Constants;
import com.badlogic.util.ImageLoader;
import com.badlogic.util.SpriteKeys;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Map {
    private int width;
    private int height;
    private GameManager gameManager;
    private int[][] mapTemplate;

    public Map(int width, int height, GameManager gameManager) {
        this.width = width;
        this.height = height;
        this.gameManager = gameManager;
        this.mapTemplate = new int[width][height];
        this.loadFromTemplate("template_one.png");
    }

    public void render(Graphics graphics) {
        for (int i = 0; i < this.width; i++) {
            for (int j = 0; j < this.height; j++) {
                var camOffset = gameManager.getCamera().getOffset();
                int posX = (i * Constants.SPRITE_WIDTH - (int) camOffset.getX()) - 64;
                int posY = (j * Constants.SPRITE_HEIGHT - (int) camOffset.getY()) - 64;
                int color = mapTemplate[i][j];
                BufferedImage image = null;

                if (color == Color.blue.getRGB())
                    image = Assets.getSprite(SpriteKeys.DARK_TILE);
                else if (color == Color.green.getRGB())
                    image = Assets.getSprite(SpriteKeys.NEUTRAL_TILE);
                else
                    image = Assets.getSprite(SpriteKeys.LIGHT_TILE);

                graphics.drawImage(image, posX, posY, null);
            }
        }
    }

    private void loadFromTemplate(String name) {
        var image = ImageLoader.loadImage(Constants.MAPS + "/" + name);

        for (var i = 0; i < this.width; i++) {
            for (var j = 0; j < this.height; j++) {
                mapTemplate[i][j] = image.getRGB(i, j);
            }
        }
    }
}
