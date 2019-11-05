using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class WeaponDecorator : IWeapon
    {
        protected IWeapon _weapon;

        public WeaponDecorator(IWeapon w)
        {
            _weapon = w;
        }

        public int getAmmo()
        {
            return _weapon.getAmmo();
        }
    }
}