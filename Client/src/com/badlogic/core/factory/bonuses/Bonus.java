package com.badlogic.core.factory.bonuses;

import com.badlogic.game.Player;

public abstract class Bonus {
    protected int lifespan;
    protected int bonusAmount;
    public abstract void applyBonus(Player player);
    public abstract int getBonus();
    public abstract int getLifespan();
    public abstract void setLifespan(int lifespan);
    public abstract void setBonusAmount(int bonusAmount);
}
