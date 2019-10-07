namespace Server.Game.Entities
{
    public static class Map
    {
        public static readonly int
            Width = Constants.MapWidth * Constants.MapTileSize,
            Height = Constants.MapHeight * Constants.MapTileSize,
            CenterX = Width / 2 - Constants.MapTileSize / 2,
            CenterY = Height / 2 - Constants.MapTileSize / 2;
    }
}