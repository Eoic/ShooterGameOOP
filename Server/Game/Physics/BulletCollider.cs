using System;
using Server.Game.Entities;

namespace Server.Game.Physics
{
    public class BulletCollider : Collider
    {
        public bool IsColliding(GameObject subject, GameObject obstacle) =>
            (Math.Pow(subject.Position.X - obstacle.Position.X, 2) +
             Math.Pow(subject.Position.Y - obstacle.Position.Y, 2) <
             Math.Pow(Constants.MapTileHalfSize, 2));   

        public sealed override bool ProcessMotion(long delta, GameObject subject) =>
            base.ProcessMotion(delta, subject);

        public override bool IsNextPositionValid(Vector nextPosition)
        {
            var x = nextPosition.X;
            var y = nextPosition.Y;

            return !(x < -Constants.MapTileHalfSize ||
                    x > Constants.MapWidth * Constants.MapTileSize - Constants.MapTileHalfSize ||
                    y < -Constants.MapTileHalfSize ||
                    y > Constants.MapHeight * Constants.MapTileSize - Constants.MapTileHalfSize);
        }

        public override void Move(Vector change, GameObject subject) =>
            subject.Position.Add(change);

        public override Vector GetNextPosition(GameObject subject, Vector change) =>
            subject.Position + change;
        
        public override Vector GetPositionChange(GameObject subject, long delta) => 
            subject.Direction * Constants.DefaultSpeed * delta;
    }
}