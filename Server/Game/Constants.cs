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
            MaxPlayerPerTeam = MaxPlayerCount / 2,
            DefaultSpeed = 1,

            //Healt Points
            BonusHP = 30,
            MaxHP = 100,
            MinHP = 0;
    }
}