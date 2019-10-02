package com.badlogic.util;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class Point {
    @JsonProperty("X")
    private double x;
    @JsonProperty("Y")
    private double y;

    @JsonCreator
    public Point(@JsonProperty("X") int x, @JsonProperty("Y") int y) {
        this.x = x;
        this.y = y;
    }

    public double getX() {
        return x;
    }

    public double getY() {
        return y;
    }
}

