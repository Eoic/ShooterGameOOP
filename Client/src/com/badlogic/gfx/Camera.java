package com.badlogic.gfx;

import com.badlogic.core.GameObject;
import com.badlogic.util.Vector;

import java.awt.*;

public class Camera {
    private Vector offset;

    public Camera() {
        offset = new Vector();
    }

    public Vector getOffset() {
        return offset;
    }

    public void follow(GameObject gameObject, Dimension windowSize) {
        offset.set(gameObject.getPosition().difference(windowSize.getWidth() / 2.0, windowSize.getHeight() / 2.0));
    }
}
