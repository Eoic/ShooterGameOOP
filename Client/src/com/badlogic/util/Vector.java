package com.badlogic.util;

public class Vector {
    private double x;
    private double y;
    public static final Vector LEFT = new Vector(-1, 0),
                               RIGHT = new Vector(1, 0),
                               UP = new Vector(0, -1),
                               DOWN = new Vector(0, 1);

    public Vector() {
        this.x = 0;
        this.y = 0;
    }

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

    public void set(int x, int y) {
        this.x = x;
        this.y = y;
    }

    public Vector sum(Vector vector) {
        return new Vector(this.x + vector.x, this.y + vector.y);
    }

    public Vector difference(Vector vector) {
        return new Vector(this.x - vector.getX(), this.y - vector.getY());
    }

    public Vector difference(double x, double y) {
        return new Vector(this.x - x, this.y - y);
    }

    public void add(double x, double y) {
        this.x += x;
        this.y += y;
    }

    public void subtract(double x, double y) {
        this.x -= x;
        this.y -= y;
    }

    public void add(Vector vector) {
        this.x += vector.getX();
        this.y += vector.getY();
    }

    public Vector multiply(double scalar) {
        return new Vector(this.x * scalar, this.y * scalar);
    }

    public Vector multiply(Vector vector) {
        return new Vector(this.x * vector.getX(), this.y * vector.getY());
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

    public static Vector fromPoint(Point point) {
        return new Vector(point.getX(), point.getY());
    }

    public Point getSerializable() {
        return new Point(this.x, this.y);
    }
}
