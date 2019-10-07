package com.badlogic.serializables;

import com.badlogic.util.Point;
import com.fasterxml.jackson.annotation.JsonCreator;
import com.fasterxml.jackson.annotation.JsonProperty;

public class SerializableBonus {
    @JsonProperty("BonusAmount")
    private int bonusAmount;

    @JsonProperty("Lifespan")
    private int lifespan;

    @JsonProperty("Position")
    private Point position;

    @JsonProperty("Type")
    private String type;

    public SerializableBonus() {}

    @JsonCreator
    public SerializableBonus(@JsonProperty("BonusAmount") int bonusAmount,
                             @JsonProperty("Lifespan") int lifespan,
                             @JsonProperty("Position") Point position,
                             @JsonProperty("Type") String type) {
        this.bonusAmount = bonusAmount;
        this.lifespan = lifespan;
        this.position = position;
        this.type = type;
    }

    public int getBonusAmount() {
        return bonusAmount;
    }

    public int getLifespan() {
        return lifespan;
    }

    public Point getPosition() {
        return position;
    }

    public String getType() {
        return type;
    }
}
