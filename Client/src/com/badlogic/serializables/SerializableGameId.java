package com.badlogic.serializables;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializableGameId {
    @JsonProperty("RoomId")
    private String roomId;

    @JsonCreator
    public SerializableGameId(@JsonProperty("RoomId") String roomId) {
        this.roomId = roomId;
    }
}
