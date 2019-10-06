package com.badlogic.core.factory;

import com.badlogic.core.factory.bonuses.BonusFactory;

public class FactoryProvider {
    public static AbstractFactory getFactory(Factory factory) {
        switch (factory) {
            case BONUS:
                return new BonusFactory();
            default:
                return null;
        }
    }
}
