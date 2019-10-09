namespace Server.Game
{
    public static class Constants
    {
        public static readonly int
            //Game
            Fps = 60,
            PlayerUpdateInterval = 60,
            RoomUpdateInterval = 20,

            //Players
            MaxPlayerCount = 10,
            MaxPlayersPerTeam = MaxPlayerCount / 2,
            DefaultSpeed = 1,

            //Health Points
            BonusHealth = 30,
            MaxHealth = 100,
            MinHealth = 0,

            // Bonuses
            BonusTypeCount = 3,
            MinBonusCount = 1,
            MaxBonusCount = 10,
            BonusMinLifespan = 1,
            BonusMaxLifespan = 100,
            BonusMinAmount = 5,
            BonusMaxAmount = 100,

            // Map
            MapTileSize = 128,
            MapWidth = 20,
            MapHeight = 10,
            MapBoundOffset = 64;
    }
}