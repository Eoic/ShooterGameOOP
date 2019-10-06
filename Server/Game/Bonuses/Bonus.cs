using Server.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Game.Bonuses
{
    public abstract class Bonus : GameObject
    {
        public int Lifespan { get; private set; }
        public int BonusAmount { get; private set; }

        public Bonus() { }

        public Bonus(int lifespan, int bonusAmount)
        {
            Lifespan = lifespan;
            BonusAmount = bonusAmount;
        }

        public abstract void ApplyBonus(Player player);
    }
}