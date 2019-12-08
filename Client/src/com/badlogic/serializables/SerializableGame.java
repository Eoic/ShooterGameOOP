package com.badlogic.serializables;

import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializableGame {
    @JsonProperty("RoomId")
    private String roomId;

    @JsonProperty("JoinedPlayers")
    private int joinedPlayers;

    @JsonProperty("MaxPlayers")
    private int maxPlayers;

    public SerializableGame(@JsonProperty("RoomId") String roomId,
                            @JsonProperty("JoinedPlayers") int joinedPlayers,
                            @JsonProperty("MaxPlayers") int maxPlayers)                          {
        this.roomId = roomId;
        this.joinedPlayers = joinedPlayers;
        this.maxPlayers = maxPlayers;
    }

    public String getRoomId() {
        return roomId;
    }

    public int getJoinedPlayers() {
        return joinedPlayers;
    }

    public int getMaxPlayers() {
        return maxPlayers;
    }
}
