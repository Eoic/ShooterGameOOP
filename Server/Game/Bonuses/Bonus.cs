using Server.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Game.Bonuses
{
    public abstract class Bonus : GameObject
    {
        public int Lifespan { get; }
        public int BonusAmount { get; }

        public Bonus() { }

        public Bonus(int lifespan, int bonusAmount)
        {
            Lifespan = lifespan;
            BonusAmount = bonusAmount;
        }

        public abstract void ApplyBonus(Player player);
    }
}