using Server.Game;

namespace Server.Models.GunFactory
{
    public class SniperRifleFactory : AbstractFactory
    {
        public override Bullet CreateBullet() =>
            new Bullet(Constants.SniperRifleDamage, GunTypes.GetGunType(GunType.SniperRifle));

        public override Weapon CreateWeapon()
        {
            return Weapon.Builder.GetInstance()
                  .SetName(GunTypes.GetGunType(GunType.SniperRifle))
                  .SetType(GunTypes.GetGunType(GunType.SniperRifle))
                  .SetBullets(CreateBullet()).
                  Build();
        }
    }
}