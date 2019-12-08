namespace Server.Models.GunFactory
{
    public abstract class AbstractFactory : IWeaponFactoryProxy
    {
        public abstract Weapon CreateWeapon();

        public abstract Bullet CreateBullet();
    }
}