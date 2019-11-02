namespace Server.Models
{
    public class ShotgunFactory : AbstractFactory
    {
        private new int defaultGunType = 2;

        public override Bullet createBullet()
        {
            return new Bullet(20, GunTypes.getGunType(defaultGunType));
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