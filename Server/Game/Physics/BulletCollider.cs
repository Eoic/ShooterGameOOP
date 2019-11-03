using System;
using Server.Game.Entities;

namespace Server.Game.Physics
{
    public class BulletCollider : ICollider
    {
        public bool IsColliding(GameObject subject, GameObject obstacle)
        {
            throw new NotImplementedException();
        }

        public bool WillCollide(Vector nextPosition, GameObject obstacle)
        {
            throw new NotImplementedException();
        }

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