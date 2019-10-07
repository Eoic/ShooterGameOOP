package com.badlogic.core.factory;

import com.badlogic.game.GameManager;
import com.badlogic.gfx.Assets;

import java.awt.image.BufferedImage;

public class BonusFactory {
    public static Bonus create(String bonusType, GameManager gameManager) {
        switch (bonusType) {
            case BonusType.AMMO:
                return new AmmoBonus(gameManager, Assets.getSprite("ammoBonus"));
            case BonusType.HEALTH:
                return new HealthBonus(gameManager, Assets.getSprite("healthBonus"));
            case BonusType.SPEED:
                return new SpeedBonus(gameManager, Assets.getSprite("speedBonus"));
            default:
                return new NullBonus(gameManager, null);
        }
    }
}
