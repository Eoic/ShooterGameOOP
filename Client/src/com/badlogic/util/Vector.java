package com.badlogic.util;

public class Vector {
    public static final Vector ZERO = new Vector(0, 0);
    public static final Vector ONE = new Vector(1, 1);
    private double x;
    private double y;

    public Vector(double x, double y) {
        this.x = x;
        this.y = y;
    }

    public double getX() { return x; }

    public double getY() {
        return y;
    }

    public void setX(double x) { this.x = x; }

    public void setY(double y) { this.y = y; }

    public void set(Vector vector) {
        this.x = vector.getX();
        this.y = vector.getY();
    }

    public void add(double x, double y) {
        this.x += x;
        this.y += y;
    }

    public void add(Vector vector) {
        this.x += vector.getX();
        this.y += vector.getY();
    }

    public Vector multiply(double scalar) {
        return new Vector(this.x * scalar, this.y * scalar);
    }

    public double getMagnitude() {
        return Math.sqrt(Math.pow(this.x, 2) + Math.pow(this.y, 2));
    }

    public Vector getNormalized() {
        double magnitude = this.getMagnitude();
        return new Vector(this.x / magnitude, this.y / magnitude);
    }

    @Override
    public String toString() {
        return "(x: " + this.x + ", y: " + this.y + ")";
    }

    @Override
    public boolean equals(Object obj) {
        if (this == obj)
            return true;

        if (obj == null || obj.getClass() != this.getClass())
            return false;

        var pos = (Vector)obj;
        return this.x == pos.getX() && this.y == pos.getY();
    }

    public Point dump() {
        return new Point(this.x, this.y);
    }
}
