package com.badlogic.core.factory;

public interface AbstractFactory<FactoryType> {
    FactoryType create(String factoryObjectType);
}
