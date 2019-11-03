namespace Server.Models.GunFactory
{
    public abstract class AbstractFactory
    {
        public abstract Weapon CreateWeapon();

        public abstract Bullet CreateBullet();
    }
}