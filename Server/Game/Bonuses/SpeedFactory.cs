namespace Server.Game.Bonuses
{
    public class SpeedFactory : AbstractFactory
    {
        public override Bonus GetBonus()
        {
            return new SpeedBonus();
        }
    }
}