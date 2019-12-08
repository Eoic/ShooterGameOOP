using Server.Game.Entities;
using System.Diagnostics;

namespace Server.Game.Physics
{
    public class PlayerCollider : Collider
    {
        public override sealed bool ProcessMotion(long delta, GameObject subject) =>
            base.ProcessMotion(delta, subject);

        public override bool IsNextPositionValid(Vector nextPosition)
        {
            /*
            var willTouchObstacle  = false;

            Map.obstacles.ForEach((obstacle) =>
            {
                var x0 = obstacle.X * Constants.MapTileHalfSize;
                var y0 = obstacle.Y * Constants.MapTileHalfSize;

                var noCollisionByX = (nextPosition.X > (x0 - Constants.MapTileHalfSize)) || (nextPosition.X > (x0 + Constants.MapTileHalfSize));
                var noCollisionByY = (nextPosition.Y > (y0 - Constants.MapTileHalfSize)) || (nextPosition.Y > (y0 + Constants.MapTileHalfSize));
                willTouchObstacle = !(noCollisionByX || noCollisionByY);

                if (willTouchObstacle)
                {
                    Debug.WriteLine(x0 + " " + y0);
                }

                Debug.WriteLine("----");
            });
            */
            
            return (nextPosition.X <= Map.Width - Constants.MapTileSize && nextPosition.X >= 0 &&
             nextPosition.Y <= Map.Height - Constants.MapTileSize && nextPosition.Y >= 0);
        }

        public override void Move(Vector change, GameObject subject) =>
            subject.Position.Add(change);

        public override Vector GetNextPosition(GameObject subject, Vector change) =>
            subject.Position + change;

        public override Vector GetPositionChange(GameObject subject, long delta) =>
            subject.Direction * Constants.DefaultSpeed * delta;
    }
}