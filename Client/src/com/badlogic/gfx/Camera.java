package com.badlogic.gfx;

import com.badlogic.core.Entity;
import com.badlogic.util.Vector;

public class Camera {
    private Vector offset = Vector.ZERO;

    public Camera() { }
    public Camera (Vector offset) {
        this.offset = offset;
    }

    public Vector getOffset() {
        return offset;
    }

    public void setPivot(Entity entity) {
        // this.offset.setX(this.offset.getX() - 800 / 2 + entity.getWidth() / 2);
        // this.offset.setY(this.offset.getY() - 600 / 2 + entity.getHeight() / 2);
    }
}
