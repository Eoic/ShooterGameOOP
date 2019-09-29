package com.badlogic.core.factory.weapons;

import com.badlogic.core.factory.AbstractFactory;

public class WeaponFactory implements AbstractFactory<Weapon> {
    @Override
    public Weapon create(String weaponType) {
        switch (weaponType) {
            case WeaponType.PISTOL:
                return new Pistol();
            case WeaponType.RIFLE:
                return new Rifle();
            case WeaponType.SHOTGUN:
                return new Shotgun();
            default:
                return new NullWeapon();
        }
    }
}
