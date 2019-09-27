package com.badlogic.util;

public class Vector {
    public static final Vector ZERO = new Vector(0, 0);
    public static final Vector ONE = new Vector(1, 1);
    private int x;
    private int y;

    public Vector(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public int getX() { return x; }

    public int getY() {
        return y;
    }

    public void add(int x, int y) {
        this.x += x;
        this.y += y;
    }
}
