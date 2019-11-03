using Server.Game.Entities;

namespace Server.Game.Bonuses
{
    public abstract class Bonus : GameObject
    {
        public string Type { get; }
        public int Lifespan { get; }
        public int BonusAmount { get; }

        protected Bonus(string type, int lifespan, int bonusAmount)
        {
            Type = type;
            Lifespan = lifespan;
            BonusAmount = bonusAmount;
        }

        public abstract void ApplyBonus(Player player);

        public SerializableBonus GetSerializable() => 
            new SerializableBonus(Lifespan, BonusAmount, Position, Type);
    }
}