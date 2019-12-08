using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Server.Game.Entities;

namespace Server.Game.Bonuses
{
    [DataContract]
    public class HealthBonus : Bonus
    {
        public HealthBonus(string bonusType, int lifespan, int bonusAmount) : base(bonusType, lifespan, bonusAmount) { }

        public override void ApplyBonus(Player player)
        {
            Debug.WriteLine("Using health bonus");
        }

        public override void Update(long delta)
        {
            throw new NotImplementedException();
        }
    }
}