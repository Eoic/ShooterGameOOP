namespace Server.Game.Bonuses
{
    public class AbstractFactory
    {
        public virtual Bonus GetBonus()
        {
            return null;
        }
    }
}