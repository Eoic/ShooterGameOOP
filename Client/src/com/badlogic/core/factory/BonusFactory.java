package com.badlogic.core.factory;

import com.badlogic.game.GameManager;

import java.awt.image.BufferedImage;

public class BonusFactory {
    public static Bonus create(String bonusType, GameManager gameManager, BufferedImage sprite) {
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
