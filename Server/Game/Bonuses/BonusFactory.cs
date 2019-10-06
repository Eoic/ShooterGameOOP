using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Game.Bonuses
{
    public static class BonusFactory
    {
        public static Bonus GetBonus(string bonusType)
        {
            switch (bonusType)
            {
                case BonusType.Health:
                    return new HealthBonus();
                case BonusType.Ammo:
                    return new AmmoBonus();
                case BonusType.Speed:
                    return new SpeedBonus();
                default:
                    return null;
            }
        }
    }
}