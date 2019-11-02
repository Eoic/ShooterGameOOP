namespace Server.Models
{
    public class ShotgunFactory : AbstractFactory
    {
        private new int defaultGunType = 2;

        public override Bullet createBullet()
        {
            return new Bullet(20, GunTypes.GetGunType(defaultGunType));
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