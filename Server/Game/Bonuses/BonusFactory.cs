using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Game.Bonuses
{
    public static class BonusFactory
    {
        public static Bonus GetBonus(string bonusType, int bonusAmount = 0, int lifespan = 0)
        {
            switch (bonusType)
            {
                case BonusType.Health:
                    return new HealthBonus(bonusAmount, lifespan, bonusType);
                case BonusType.Ammo:
                    return new AmmoBonus(bonusAmount, lifespan);
                case BonusType.Speed:
                    return new SpeedBonus(bonusAmount, lifespan);
                default:
                    return null;
            }
        }
    }
}