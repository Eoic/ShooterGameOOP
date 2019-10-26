using Server.Game.Entities;

namespace Server.Game.Bonuses
{
    public abstract class Bonus : GameObject
    {
        public int Lifespan { get; }
        public int BonusAmount { get; }

        protected Bonus() { }

        protected Bonus(int lifespan, int bonusAmount)
        {
            Lifespan = lifespan;
            BonusAmount = bonusAmount;
        }

        public abstract void ApplyBonus(Player player);

        public SerializableBonus GetSerializable(string bonusType) => 
            new SerializableBonus(Lifespan, BonusAmount, Position, bonusType);
    }
}