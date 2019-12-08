package com.badlogic.serializables;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializablePlayerState {
    @JsonProperty("Name")
    private String name;

    @JsonProperty("Team")
    private int team;

    @JsonProperty("Health")
    private int health;

    @JsonCreator
    public SerializablePlayerState(@JsonProperty("Name") String name,
                                   @JsonProperty("Team") int team,
                                   @JsonProperty("Health") int health) {
        this.name = name;
        this.team = team;
        this.health = health;
    }

    public String getName() {
        return name;
    }

    public int getTeam() {
        return team;
    }

    public int getHealth() {
        return health;
    }
}
