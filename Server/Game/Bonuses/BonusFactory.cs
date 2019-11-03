namespace Server.Game.Bonuses
{
    public static class BonusFactory
    {
        public static Bonus GetBonus(string bonusType, int bonusAmount = 0, int lifespan = 0)
        {
            switch (bonusType)
            {
                case BonusType.Health:
                    return new HealthBonus(bonusType, bonusAmount, lifespan);
                case BonusType.Ammo:
                    return new AmmoBonus(bonusType, bonusAmount, lifespan);
                case BonusType.Speed:
                    return new SpeedBonus(bonusType, bonusAmount, lifespan);
                default:
                    return null;
            }
        }
    }
}