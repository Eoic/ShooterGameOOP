package com.badlogic.core.factory.weapons;

public class NullWeapon extends Weapon {
    @Override
    public void shoot() {

    }

    @Override
    public int getAmmo() {
        return 0;
    }

    @Override
    public int getDamage() {
        return 0;
    }

    @Override
    public int getShotTimeout() {
        return 0;
    }

    @Override
    public void setAmmo(int ammo) {

    }

    @Override
    public void setDamage(int damage) {

    }

    @Override
    public void setShotTimeout(int shotTimeout) {

    }
}
