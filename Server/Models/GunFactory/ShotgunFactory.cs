using System;
using Server.Game;

namespace Server.Models.GunFactory
{
    public class ShotgunFactory : AbstractFactory
    {
        public override Bullet CreateBullet() =>
            new Bullet(Constants.ShotgunDamage, GunTypes.GetGunType(GunType.Shotgun));
    
        public override Weapon CreateWeapon()
        {
            return Weapon.Builder.GetInstance()
                .SetName(GunTypes.GetGunType(GunType.Shotgun) + Guid.NewGuid())
                .SetType(GunTypes.GetGunType(GunType.Shotgun))
                .SetBullets(CreateBullet()).
                Build();
        }
    }
}