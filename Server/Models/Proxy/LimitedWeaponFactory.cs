using Server.Models.GunFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Models.Proxy
{
    class LimitedWeaponFactory : IWeaponFactoryProxy
    {
        private const int MaxWeapons = 20;
        private int Count = 0;

        private AbstractFactory Factory;


        public LimitedWeaponFactory(AbstractFactory factory)
        {
            Factory = factory;
        }

        public Bullet CreateBullet()
        {
            return Factory.CreateBullet();
        }

        public Weapon CreateWeapon()
        {
            if (Count < MaxWeapons)
            {
                return Factory.CreateWeapon();
            }

            return null;
        }
    }
}
