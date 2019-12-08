using Server.Game.Entities;
using System;
using System.Collections.Generic;

namespace Server.Game.Bonuses
{
    public class BonusGenerator
    {
        public static List<Bonus> Create()
        {
            var bonusList = new List<Bonus>();
            var randomGen = new Random(DateTime.Now.Millisecond);
            var iterations = randomGen.Next(Constants.MinBonusCount, Constants.MaxBonusCount);

            for (var i = 0; i < iterations; i++)
            {
                var bonusType = string.Empty;
                var bonusAmount = randomGen.Next(Constants.BonusMinAmount, Constants.BonusMaxAmount + 1);
                var bonusLifespan = randomGen.Next(Constants.BonusMinLifespan, Constants.BonusMaxLifespan + 1);

                switch (randomGen.Next(Constants.BonusTypeCount))
                {
                    case 0:
                        bonusType = BonusType.Health;
                        break;
                    case 1:
                        bonusType = BonusType.Ammo;
                        break;
                    case 2:
                        bonusType = BonusType.Speed;
                        break;
                    default:
                        System.Diagnostics.Debug.WriteLine("Unrecognized bonus type");
                        break;
                }

                if (string.IsNullOrEmpty(bonusType))
                    continue;

                var bonus = BonusFactory.GetBonus(bonusType, bonusAmount, bonusLifespan);
                bonus.Position = new Vector(randomGen.Next(0, Map.Width - Constants.MapBoundOffset), randomGen.Next(0, Map.Height - Constants.MapBoundOffset));
                bonusList.Add(bonus);
            }

            return bonusList;
        }
    }
}