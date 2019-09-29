package com.badlogic.core.factory.bonuses;

import com.badlogic.core.factory.AbstractFactory;

public class BonusFactory implements AbstractFactory<Bonus> {
    @Override
    public Bonus create(String bonusType) {
        switch (bonusType) {
            case BonusType.AMMO:
                return new AmmoBonus();
            case BonusType.HEALTH:
                return new HealthBonus();
            case BonusType.SPEED:
                return new SpeedBonus();
            default:
                return new NullBonus();
        }
    }
}
