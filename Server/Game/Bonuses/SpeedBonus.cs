using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Server.Game.Entities;

namespace Server.Game.Bonuses
{
    public class SpeedBonus : Bonus
    {
        public SpeedBonus() { }

        public SpeedBonus(int lifespan, int bonusAmount) : base(lifespan, bonusAmount) { }

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