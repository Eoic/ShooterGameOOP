namespace Server.Models
{
    public class SniperRifleFactory : AbstractFactory
    {
        private new int defaultGunType = 1;

        public override Bullet createBullet()
        {
            return new Bullet(50, GunTypes.GetGunType(defaultGunType));
        }

        public override Weapon createWeapon()
        {
            return Weapon.Builder.GetInstance()
                  .setName("Test " + GunTypes.GetGunType(defaultGunType))
                  .setType(GunTypes.GetGunType(defaultGunType))
                  .setBulets(createBullet()).
                  build();
        }
    }
}