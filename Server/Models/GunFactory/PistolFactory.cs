using System;
using Server.Game;

namespace Server.Models.GunFactory
{
    public class PistolFactory : AbstractFactory
    {
        public override Bullet CreateBullet() =>
            new Bullet(Constants.PistolDamage, GunTypes.GetGunType(GunType.Pistol));

        public override Weapon CreateWeapon()
        {
            var weapon = new Weapon().Clone();
            weapon.Bullets = CreateBullet();
            weapon.Name = GunTypes.GetGunType(GunType.Pistol) + Guid.NewGuid();
            return weapon;
        }
    }
}