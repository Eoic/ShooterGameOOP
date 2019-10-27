package com.badlogic.serializables;

import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializableGame {
    @JsonProperty("GameId")
    private String gameId;

    @JsonProperty("JoinedPlayers")
    private int joinedPlayers;

    @JsonProperty("MaxPlayers")
    private int maxPlayers;

    public SerializableGame(@JsonProperty("GameId") String gameId,
                            @JsonProperty("JoinedPlayers") int joinedPlayers,
                            @JsonProperty("MaxPlayers") int maxPlayers)                          {
        this.gameId = gameId;
        this.joinedPlayers = joinedPlayers;
        this.maxPlayers = maxPlayers;
    }

    public String getGameId() {
        return gameId;
    }

    public int getJoinedPlayers() {
        return joinedPlayers;
    }

    public int getMaxPlayers() {
        return maxPlayers;
    }
}
