package com.badlogic.gfx;

import com.badlogic.game.GameManager;
import com.badlogic.util.Constants;
import com.badlogic.util.ImageLoader;
import com.badlogic.util.SpriteKeys;

import java.awt.*;
import java.awt.image.BufferedImage;
import java.util.ArrayList;

public class Map {
    private int width;
    private int height;
    private GameManager gameManager;
    private int[][] mapTemplate;
    private Tile[][] map;

    public Map(int width, int height, GameManager gameManager) {
        this.width = width;
        this.height = height;
        this.gameManager = gameManager;
        this.mapTemplate = new int[width][height];
        this.map = new Tile[width][height];
        this.loadFromTemplate("template_one.png");
        this.buildMapImage();
    }

    private void buildMapImage() {
        var darkTile = Assets.getSprite(SpriteKeys.DARK_TILE);
        var neutralTile = Assets.getSprite(SpriteKeys.NEUTRAL_TILE);
        var lightTile = Assets.getSprite(SpriteKeys.LIGHT_TILE);

        for (int i = 0; i < this.width; i++) {
            for (int j = 0; j < this.height; j++) {
                BufferedImage image;
                var basePosX = i * Constants.SPRITE_WIDTH;
                var basePosY = j * Constants.SPRITE_HEIGHT;
                int color = mapTemplate[i][j];

                if (color == Color.blue.getRGB())
                    image = darkTile;
                else if (color == Color.green.getRGB())
                    image = neutralTile;
                else
                    image = lightTile;

                map[i][j] = new Tile(image, basePosX, basePosY);
            }
        }
    }

    public void render(Graphics graphics) {
        for (int i = 0; i < this.width; i++) {
            for (int j = 0; j < this.height; j++) {
                var camOffset = gameManager.getCamera().getOffset();
                map[i][j].render(graphics, camOffset);
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
