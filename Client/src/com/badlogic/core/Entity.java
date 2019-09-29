package com.badlogic.core;

import com.badlogic.util.Vector;

import java.awt.*;
import java.util.UUID;

public abstract class Entity {
    protected String id = UUID.randomUUID().toString();
    protected Vector position = Vector.ZERO;
    public abstract Vector getPosition();
    public abstract String getId();
    public abstract void render(Graphics graphics);
    public abstract void update(int delta);
}
