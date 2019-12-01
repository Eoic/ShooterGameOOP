namespace Server.Game
{
    public static class Constants
    {
        public static readonly int
            //Game
            Fps = 120,
            PlayerUpdateInterval = 120,
            RoomUpdateInterval = 120,

            //Players
            MaxPlayerCount = 10,
            MaxPlayersPerTeam = MaxPlayerCount / 2,
            DefaultSpeed = 1,
            DefaultBulletSpeed = 1,
            DefaultBulletPoolSize = 100,

            //Health Points
            BonusHealth = 30,
            MaxHealth = 100,
            MinHealth = 0,

            // Bonuses
            BonusTypeCount = 3,
            MinBonusCount = 4,
            MaxBonusCount = 10,
            BonusMinLifespan = 1,
            BonusMaxLifespan = 100,
            BonusMinAmount = 5,
            BonusMaxAmount = 100,

            // Map
            MapTileSize = 128,
            MapTileHalfSize = 64,
            MapWidth = 20,
            MapHeight = 15,
            MapBoundOffset = 64,

            // Guns
            PistolDamage = 10,
            ShotgunDamage = 20,
            SniperRifleDamage = 50,

            // Game time (seconds)
            GameWaitTime = 5, // 30
            GameReadyTime = 5,
            GameDurationTime = 5, //90

            // Team types
            TeamA = 0,
            TeamB = 1;

        public static string
            WaitingForPlayers = "Waiting for players to connect",
            GameStarting = "Game starting in",
            GameEnding = "Game ends in";
    }
}