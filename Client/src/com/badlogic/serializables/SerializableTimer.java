package com.badlogic.serializables;

import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializableTimer {
    @JsonProperty("Label")
    private String label;

    @JsonProperty("Value")
    private int value;

    @JsonCreator
    public SerializableTimer(@JsonProperty("Label") String label,
                             @JsonProperty("Value") int value) {
        this.label = label;
        this.value = value;
    }

    public int getValue() {
        return value;
    }

    public void setValue(int value) {
        this.value = value;
    }

    public String getLabel() {
        return label;
    }

    public void setLabel(String label) {
        this.label = label;
    }
}
