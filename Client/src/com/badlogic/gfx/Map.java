package com.badlogic.gfx;

import com.badlogic.util.Constants;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Map {
    private int width;
    private int height;
    private Camera camera;

    public Map(int width, int height, Camera camera) {
        this.width = width;
        this.height = height;
        this.camera = camera;
    }

    public void render(BufferedImage tile,  Graphics graphics) {
        for (int i = 0; i < this.width; i++) {
            for (int j = 0; j < height; j++) {
                var camOffset = camera.getOffset();
                int posX = i * Constants.SPRITE_WIDTH - (int)camOffset.getX();
                int posY = j * Constants.SPRITE_HEIGHT - (int)camOffset.getY();
                graphics.drawImage(tile, posX, posY, null);
            }
        }
    }
}
