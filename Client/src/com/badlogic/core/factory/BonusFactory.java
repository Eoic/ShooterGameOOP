package com.badlogic.core.factory;

import com.badlogic.game.GameManager;
import com.badlogic.gfx.Assets;
import com.badlogic.util.SpriteKeys;

public class BonusFactory {
    public static Bonus create(String bonusType, GameManager gameManager) {
        switch (bonusType) {
            case BonusType.AMMO:
                return new AmmoBonus(gameManager, Assets.getSprite(SpriteKeys.AMMO_BONUS));
            case BonusType.HEALTH:
                return new HealthBonus(gameManager, Assets.getSprite(SpriteKeys.HEALTH_BONUS));
            case BonusType.SPEED:
                return new SpeedBonus(gameManager, Assets.getSprite(SpriteKeys.SPEED_BONUS));
            default:
                return new NullBonus(gameManager, null);
        }
    }
}
