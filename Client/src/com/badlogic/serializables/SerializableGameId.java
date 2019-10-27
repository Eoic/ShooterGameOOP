package com.badlogic.serializables;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializableGameId {
    @JsonProperty("GameId")
    private String gameId;

    @JsonCreator
    public SerializableGameId(@JsonProperty("GameId") String gameId) {
        this.gameId = gameId;
    }
}
