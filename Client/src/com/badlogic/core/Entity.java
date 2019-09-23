package com.badlogic.core;

import com.badlogic.util.Vector;

public class Entity {
    private Vector position;

    public Entity() {
        position = Vector.ZERO;
    }

    public Vector getPosition() {
        return position;
    }
}
