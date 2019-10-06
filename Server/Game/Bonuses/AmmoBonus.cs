using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Server.Game.Entities;

namespace Server.Game.Bonuses
{
    public class AmmoBonus : Bonus
    {
        public override void ApplyBonus(Player player)
        {
            Debug.WriteLine("Using ammo bonus");
        }

        public override void Update(long delta)
        {
            throw new NotImplementedException();
        }
    }
}