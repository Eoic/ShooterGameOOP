package com.badlogic.core;

import com.badlogic.game.GameManager;
import com.badlogic.util.Constants;
import com.badlogic.util.Vector;

import java.awt.*;
import java.awt.image.BufferedImage;
import java.util.UUID;

public abstract class GameObject {
    protected String id = UUID.randomUUID().toString();
    protected Vector position;
    protected GameManager gameManager;
    protected BufferedImage sprite;

    public Vector getPosition() {
        return this.position;
    }

    public String getId() {
        return this.id;
    }

    public abstract void render(Graphics graphics);

    public abstract void update(int delta);
}
