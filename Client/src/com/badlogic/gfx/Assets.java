package com.badlogic.gfx;

import com.badlogic.util.AssetsLoader;
import com.badlogic.util.Constants;
import com.badlogic.util.ImageLoader;

import java.awt.image.BufferedImage;
import java.util.HashMap;

public class Assets {
    private static HashMap<String, BufferedImage> sprites = new HashMap<>();

    public static void load() {
        SpriteSheet spriteSheet = new SpriteSheet(ImageLoader.loadImage(Constants.SPRITE_SHEET_PATH), Constants.SHEET_ROWS, Constants.SHEET_COLUMNS);
        sprites = AssetsLoader.load(spriteSheet, Constants.SPRITE_SHEET_INFO, Constants.SPRITE_WIDTH, Constants.SPRITE_HEIGHT);
    }

    public static BufferedImage getSprite(String name) {
        return sprites.get(name);
    }
}
