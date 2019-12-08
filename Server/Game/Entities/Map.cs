using System.Collections.Generic;

namespace Server.Game.Entities
{
    public static class Map
    {
        public static readonly List<Vector> obstacles = new List<Vector>()
        {
            new Vector(4, 10),
            new Vector(4, 11),
            new Vector(5, 10),
            new Vector(6, 10),
            new Vector(7, 1),
            new Vector(7, 2),
            new Vector(7, 3),
            new Vector(9, 6),
            new Vector(10, 6),
            new Vector(13, 7),
            new Vector(13, 8),
            new Vector(13, 9),
            new Vector(15, 2),
            new Vector(16, 2),
            new Vector(16, 3),
            new Vector(16, 4),
        };

        public static readonly int
            Width = Constants.MapWidth * Constants.MapTileSize,
            Height = Constants.MapHeight * Constants.MapTileSize,
            CenterX = Width / 2 - Constants.MapTileSize / 2,
            CenterY = Height / 2 - Constants.MapTileSize / 2;
    }
}