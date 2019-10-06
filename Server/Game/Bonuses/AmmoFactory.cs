namespace Server.Game.Bonuses
{
    public class AmmoFactory : AbstractFactory
    {
        public override Bonus GetBonus()
        {
            return new AmmoBonus();
        }
    }
}