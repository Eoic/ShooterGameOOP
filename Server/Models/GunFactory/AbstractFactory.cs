namespace Server.Models
{
    public abstract class AbstractFactory
    {
        public int defaultGunType;

        public abstract Weapon createWeapon();

        public abstract Bullet createBullet();
    }
}