namespace Server.Models
{
    public class SniperRifleFactory : AbstractFactory
    {
        private new int defaultGunType = 1;

        public override Bullet createBullet()
        {
            return new Bullet(50, GunTypes.getGunType(defaultGunType));
        }

        public override Weapon createWeapon()
        {
            return Weapon.Builder.GetInstance()
                  .setName("Test " + GunTypes.getGunType(defaultGunType))
                  .setType(GunTypes.getGunType(defaultGunType))
                  .setBulets(createBullet()).
                  build();
        }
    }
}