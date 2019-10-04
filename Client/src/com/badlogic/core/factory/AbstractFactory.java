package com.badlogic.core.factory;

import com.badlogic.game.GameManager;

import java.awt.image.BufferedImage;

public interface AbstractFactory<FactoryType> {
    FactoryType create(String factoryObjectType, GameManager gameManager, BufferedImage sprite);
}
