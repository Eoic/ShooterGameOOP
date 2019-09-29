package com.badlogic.core.factory.weapons;

public abstract class Weapon {
    protected int ammo;
    protected int damage;
    protected int shotTimeout;
    public abstract void shoot();
    public abstract int getAmmo();
    public abstract int getDamage();
    public abstract int getShotTimeout();
    public abstract void setAmmo(int ammo);
    public abstract void setDamage(int damage);
    public abstract void setShotTimeout(int shotTimeout);
}
