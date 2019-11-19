﻿namespace Server.Game
{
    public static class Constants
    {
        public static readonly int
            //Game
            Fps = 60,
            PlayerUpdateInterval = 30,
            RoomUpdateInterval = 60,
            GameStartCountdown = 2000,

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
            SniperRifleDamage = 50;
    }
}