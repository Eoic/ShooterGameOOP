using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public abstract class WeaponDecorator : IWeapon
    {
        protected IWeapon weapon;

        public WeaponDecorator(IWeapon w)
        {
            weapon = w;
        }

        public abstract string getInfo();
    }
}