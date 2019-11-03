package com.badlogic.serializables;

import com.badlogic.util.Point;
import com.badlogic.util.Vector;
import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializableBullet {
    @JsonProperty("Id")
    private int id;

    @JsonProperty("Position")
    private Point position;

    @JsonProperty("Direction")
    private Point direction;

    @JsonCreator
    public SerializableBullet(@JsonProperty("Id") int id,
                              @JsonProperty("Position") Point position,
                              @JsonProperty("Direction") Point direction) {
        this.id = id;
        this.position = position;
        this.direction = direction;
    }

    public Vector getPosition() {
        return Vector.fromPoint(this.position);
    }

    public Vector getDirection() {
        return Vector.fromPoint(this.direction);
    }
}
