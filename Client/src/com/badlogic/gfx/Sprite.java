package com.badlogic.gfx;

import java.awt.image.BufferedImage;

public class Sprite {
    private String name;
    private BufferedImage texture;

    public Sprite(String name, BufferedImage texture) {
        this.name = name;
        this.texture = texture;
    }

    public BufferedImage getTexture() {
        return texture;
    }

    public String getName() {
        return name;
    }
}
