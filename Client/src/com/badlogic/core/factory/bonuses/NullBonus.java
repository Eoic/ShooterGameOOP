package com.badlogic.core.factory.bonuses;

import com.badlogic.game.Player;

public class NullBonus extends Bonus {
    @Override
    public void applyBonus(Player player) {

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

    }

    @Override
    public void setBonusAmount(int bonusAmount) {

    }
}
