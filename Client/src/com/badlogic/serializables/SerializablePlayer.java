package com.badlogic.serializables;

import com.badlogic.util.Point;
import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializablePlayer {
    @JsonProperty("Position")
    private Point position;

    @JsonProperty("Direction")
    private Point direction;

    @JsonProperty("Type")
    private int type;

    @JsonCreator
    public SerializablePlayer(@JsonProperty("Position") Point position,
                              @JsonProperty("Direction") Point direction,
                              @JsonProperty("Type") int type) {
        this.position = position;
        this.direction = direction;
        this.type = type;
    }

    public Point getPosition() {
        return position;
    }

    public Point getDirection() {
        return direction;
    }

    public int getType() {
        return type;
    }
}
