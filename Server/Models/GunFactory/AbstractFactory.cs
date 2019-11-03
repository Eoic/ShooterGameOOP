namespace Server.Models
{
    public abstract class AbstractFactory
    {
        public int defaultGunType;

        public abstract Weapon CreateWeapon();

        public abstract Bullet CreateBullet();
    }
}