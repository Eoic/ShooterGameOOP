namespace Server.Models
{
    public class ShotgunFactory : AbstractFactory
    {
        private new int defaultGunType = 2;

        public override Bullet CreateBullet()
        {
            return new Bullet(20, GunTypes.GetGunType(defaultGunType));
        }

        public override Weapon CreateWeapon()
        {
            return Weapon.Builder.GetInstance()
                .SetName("Test " + GunTypes.GetGunType(defaultGunType))
                .SetType(GunTypes.GetGunType(defaultGunType))
                .SetBullets(CreateBullet()).
                Build();
        }
    }
}