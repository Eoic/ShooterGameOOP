namespace Server.Models
{
    public class PistolFactory : AbstractFactory
    {
        public new int defaultGunType = 0;

        public override Bullet createBullet()
        {
            return new Bullet(10, GunTypes.GetGunType(defaultGunType).ToString());
        }

        public override Weapon createWeapon()
        {
            Weapon weapon = new Weapon().Clone();
            weapon.bullets = createBullet();
            weapon.Name = "Test " + GunTypes.GetGunType(defaultGunType);
            return weapon;
        }
    }
}