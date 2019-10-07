namespace Server.Game.Bonuses
{
    public class HealthFactory : AbstractFactory
    {
        public override Bonus GetBonus()
        {
            return new HealthBonus();
        }
    }
}