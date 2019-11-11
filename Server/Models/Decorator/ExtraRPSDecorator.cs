using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class ExtraRPSDecorator : WeaponDecorator
    {
        public ExtraRPSDecorator(IWeapon w) : base(w) { }

        public override string getInfo()
        {
            return base.weapon.getInfo() + " + added " + addRPS().ToString() + " RPS";
        }

        private double addRPS()
        {
            return 1.50;
        }
    }
}