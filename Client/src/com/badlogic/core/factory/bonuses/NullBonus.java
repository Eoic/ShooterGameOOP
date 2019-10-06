package com.badlogic.core.factory.bonuses;

import com.badlogic.game.GameManager;
import com.badlogic.game.Player;

import java.awt.image.BufferedImage;

public class NullBonus extends Bonus {
    public NullBonus(GameManager gameManager, BufferedImage sprite) {
        super(gameManager, sprite);
    }

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

    @Override
    public void update(int delta) {

    }
}
