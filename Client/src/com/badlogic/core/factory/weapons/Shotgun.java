package com.badlogic.core.factory.weapons;

public class Shotgun extends Weapon {
    @Override
    public void shoot() {
        System.out.println("Shotgun shooting...");
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
        this.ammo = ammo;
    }

    @Override
    public void setDamage(int damage) {
        this.damage = damage;
    }

    @Override
    public void setShotTimeout(int shotTimeout) {
        this.shotTimeout = shotTimeout;
    }
}
