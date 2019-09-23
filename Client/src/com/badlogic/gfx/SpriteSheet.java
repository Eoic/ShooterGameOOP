package com.badlogic.gfx;

import java.awt.image.BufferedImage;

public class SpriteSheet
{
    private BufferedImage spriteSheet;
    private int rows;
    private int columns;

    public SpriteSheet(BufferedImage spriteSheet, int rows, int columns) {
        this.spriteSheet = spriteSheet;
        this.columns = columns;
        this.rows = rows;
    }

    public BufferedImage parseSpriteSheet(int x, int y, int width, int height) {
        return spriteSheet.getSubimage(x, y, width, height);
    }

    public int getRows() {
        return rows;
    }

    public int getColumns() {
        return columns;
    }
}
