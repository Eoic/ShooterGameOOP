using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class ExtraAmmoDecorator : WeaponDecorator
    {
        public ExtraAmmoDecorator(IWeapon w) : base(w)
        {
            
        }
        public int getAmmo()
        {
            return base._weapon.getAmmo() + 50;
        }
    }
}