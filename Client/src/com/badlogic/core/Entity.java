package com.badlogic.core;

import com.badlogic.util.Vector;

import java.awt.*;

public interface Entity {
    Vector position = Vector.ZERO;
    Vector getPosition();
    void render(Graphics graphics);
    void update(int delta);
}
