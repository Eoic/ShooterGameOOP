using System;
using System.Diagnostics;
using Server.Game.Entities;

namespace Server.Game.Bonuses
{
    public class SpeedBonus : Bonus
    {
        public SpeedBonus(string bonusType, int lifespan, int bonusAmount) : base(bonusType, lifespan, bonusAmount) { }

        public override void ApplyBonus(Player player)
        {
            Debug.WriteLine("Using speed bonus");
        }

        public override void Update(long delta)
        {
            throw new NotImplementedException();
        }
    }
}