using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class ExtraAmmoDecorator : WeaponDecorator
    {
        public ExtraAmmoDecorator(IWeapon w) : base(w) { }

        public override string getInfo()
        {
            return weapon.getInfo() + " + added " + addAmmo().ToString() + " ammo";
        }

        private int addAmmo()
        {
            return 50;
        }
    }
}