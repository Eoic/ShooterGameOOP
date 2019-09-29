package com.badlogic.core.factory.bonuses;

import com.badlogic.game.Player;

public class HealthBonus extends Bonus {
    @Override
    public void applyBonus(Player player) {
        System.out.println("Adding " + this.bonusAmount + " health to player.");
    }

    @Override
    public int getBonus() {
        return 0;
    }

    @Override
    public int getLifespan() {
        return 0;
    }

    @Override
    public void setLifespan(int lifespan) {
        this.lifespan = lifespan;
    }

    @Override
    public void setBonusAmount(int bonusAmount) {
        this.bonusAmount = bonusAmount;
    }
}
