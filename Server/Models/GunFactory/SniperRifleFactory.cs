namespace Server.Models
{
    public class SniperRifleFactory : AbstractFactory
    {
        private new int defaultGunType = 1;

        public override Bullet CreateBullet()
        {
            return new Bullet(50, GunTypes.GetGunType(defaultGunType));
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