package com.badlogic.gfx;

import com.badlogic.util.Vector;

import java.awt.*;
import java.awt.image.BufferedImage;

public class Tile {
    private BufferedImage image;
    private int x;
    private int y;

    public Tile(BufferedImage image, int x, int y) {
        this.image = image;
        this.x = x - 64;
        this.y = y - 64;
    }

    public void render(Graphics graphics, Vector offset) {
        graphics.drawImage(image, (int)(this.x - offset.getX()), (int)(this.y - offset.getY()), null);
    }
}
