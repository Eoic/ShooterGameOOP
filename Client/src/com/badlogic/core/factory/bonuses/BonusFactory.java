package com.badlogic.core.factory.bonuses;

import com.badlogic.core.factory.AbstractFactory;
import com.badlogic.game.GameManager;

import java.awt.image.BufferedImage;

public class BonusFactory implements AbstractFactory<Bonus> {
    @Override
    public Bonus create(String bonusType, GameManager gameManager, BufferedImage sprite) {
        switch (bonusType) {
            case BonusType.AMMO:
                return new AmmoBonus(gameManager, sprite);
            case BonusType.HEALTH:
                return new HealthBonus(gameManager, sprite);
            case BonusType.SPEED:
                return new SpeedBonus(gameManager, sprite);
            default:
                return new NullBonus(gameManager, sprite);
        }
    }
}
