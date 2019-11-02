package com.badlogic.serializables;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializableGameId {
    @JsonProperty("RoomId")
    private String roomId;

    @JsonProperty("Team")
    private int team;

    @JsonCreator
    public SerializableGameId(@JsonProperty("RoomId") String roomId,
                              @JsonProperty("Team") int team) {
        this.roomId = roomId;
        this.team = team;
    }
}
