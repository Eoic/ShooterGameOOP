package com.badlogic.gfx;

import com.badlogic.util.Vector;

public class Camera {
    private Vector offset = new Vector(0, 0);

    public Camera() { }
    public Camera (Vector offset) {
        this.offset = offset;
    }

    public Vector getOffset() {
        return offset;
    }
}
