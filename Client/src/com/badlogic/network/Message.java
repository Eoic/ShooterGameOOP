package com.badlogic.network;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class Message {
    @JsonProperty("Type")
    private int type;
    @JsonProperty("Payload")
    private String payload;

    @JsonCreator
    public Message(@JsonProperty("Type") int type, @JsonProperty("Payload") String payload) {
        this.type = type;
        this.payload = payload;
    }

    public int getType() {
        return type;
    }

    public String getPayload() {
        return payload;
    }
}
