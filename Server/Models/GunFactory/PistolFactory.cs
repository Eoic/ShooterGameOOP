namespace Server.Models
{
    public class PistolFactory : AbstractFactory
    {
        public new int defaultGunType = 0;

        public override Bullet CreateBullet()
        {
            return new Bullet(10, GunTypes.GetGunType(defaultGunType).ToString());
        }

        public override Weapon CreateWeapon()
        {
            Weapon weapon = new Weapon().Clone();
            weapon.Bullets = CreateBullet();
            weapon.Name = "Test " + GunTypes.GetGunType(defaultGunType);
            return weapon;
        }
    }
}