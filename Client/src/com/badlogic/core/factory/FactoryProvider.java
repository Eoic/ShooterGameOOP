package com.badlogic.core.factory;

import com.badlogic.core.factory.bonuses.BonusFactory;
import com.badlogic.core.factory.weapons.WeaponFactory;

public class FactoryProvider {
    public static AbstractFactory getFactory(Factory factory) {
        switch (factory) {
            case BONUS:
                return new BonusFactory();
            case WEAPON:
                return new WeaponFactory();
            default:
                return null;
        }
    }
}
