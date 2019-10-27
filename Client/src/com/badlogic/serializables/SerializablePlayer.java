package com.badlogic.serializables;

import com.badlogic.util.Point;
import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializablePlayer {
    @JsonProperty("PlayerId")
    private String playerId;

    @JsonProperty("Position")
    private Point position;

    @JsonProperty("Direction")
    private Point direction;

    @JsonProperty("Type")
    private int type;

    @JsonCreator
    public SerializablePlayer(@JsonProperty("Position") Point position,
                              @JsonProperty("Direction") Point direction,
                              @JsonProperty("Type") int type,
                              @JsonProperty("PlayerId") String playerId) {
        this.position = position;
        this.direction = direction;
        this.type = type;
        this.playerId = playerId;
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

    public String getPlayerId() {
        return playerId;
    }
}
