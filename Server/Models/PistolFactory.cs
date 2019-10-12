namespace Server.Models
{
    public class PistolFactory : AbstractFactory
    {
        public new int defaultGunType = 0;


        public override Bullet createBullet()
        {
            return new Bullet(10, GunTypes.getGunType(defaultGunType).ToString());
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