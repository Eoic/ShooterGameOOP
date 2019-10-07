using System;
using System.Diagnostics;
using Server.Game.Entities;

namespace Server.Game.Bonuses
{
    public class AmmoBonus : Bonus
    {
        public AmmoBonus() { }
        public AmmoBonus(int lifespan, int bonusAmount) : base(lifespan, bonusAmount) { }

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