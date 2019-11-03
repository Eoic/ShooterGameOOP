using System;
using Server.Game.Entities;

namespace Server.Game.Physics
{
    public class BulletCollider : ICollider
    {
        public bool IsColliding(GameObject subject, GameObject obstacle) =>
            (Math.Pow(subject.Position.X - obstacle.Position.X, 2) +
             Math.Pow(subject.Position.Y - obstacle.Position.Y, 2) <
             Math.Pow(Constants.MapTileHalfSize, 2));   

        public bool WillCollide(Vector nextPosition, GameObject obstacle) => 
            (Math.Pow(nextPosition.X - obstacle.Position.X, 2) + 
             Math.Pow(nextPosition.Y - obstacle.Position.Y, 2) < 
             Math.Pow(Constants.MapTileHalfSize, 2));  

        public bool IsPositionValid(Vector nextPosition)
        {
            var x = nextPosition.X;
            var y = nextPosition.Y;

            return !(x < -Constants.MapTileHalfSize ||
                    x > Constants.MapWidth * Constants.MapTileSize - Constants.MapTileHalfSize ||
                    y < -Constants.MapTileHalfSize ||
                    y > Constants.MapHeight * Constants.MapTileSize - Constants.MapTileHalfSize);
        }

        public void Move(Vector change, GameObject subject) =>
            subject.Position.Add(change);
    }
}