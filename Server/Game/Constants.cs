namespace Server.Game
{
    public static class Constants
    {
        public static readonly int
            //Game
            Fps = 60,
            PlayerUpdateInterval = 60,

            //Players
            MaxPlayerCount = 10,
            MaxPlayersPerTeam = MaxPlayerCount / 2,
            DefaultSpeed = 1,

            //Health Points
            BonusHealth = 30,
            MaxHealth = 100,
            MinHealth = 0;
    }
}