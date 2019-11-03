package com.badlogic.serializables;

import com.badlogic.util.Point;
import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

import java.util.ArrayList;

public class SerializablePlayer {
    @JsonProperty("PlayerId")
    private String playerId;

    @JsonProperty("Position")
    private Point position;

    @JsonProperty("Direction")
    private Point direction;

    @JsonProperty("Type")
    private int type;

    @JsonProperty("Team")
    private int team;

    @JsonProperty("Bullets")
    private ArrayList<SerializableBullet> bullets;

    @JsonCreator
    public SerializablePlayer(@JsonProperty("Position") Point position,
                              @JsonProperty("Direction") Point direction,
                              @JsonProperty("Type") int type,
                              @JsonProperty("PlayerId") String playerId,
                              @JsonProperty("Team") int team,
                              @JsonProperty("Bullets") ArrayList<SerializableBullet> bullets) {
        this.position = position;
        this.direction = direction;
        this.type = type;
        this.playerId = playerId;
        this.team = team;
        this.bullets = bullets;
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

    public int getTeam() {
        return team;
    }

    public ArrayList<SerializableBullet> getBullets() {
        return bullets;
    }
}
