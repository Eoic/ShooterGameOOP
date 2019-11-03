using Server.Game.Entities;

namespace Server.Game.Physics
{
    public class PlayerCollider : ICollider
    {
        public bool IsColliding(GameObject subject, GameObject obstacle)
        {
            return false;
        }

        public bool WillCollide(Vector nextPosition, GameObject obstacle)
        {
            return false;
        }

        public bool IsPositionValid(Vector nextPosition) => 
            (nextPosition.X <= Map.Width - Constants.MapTileSize && nextPosition.X >= 0 && 
             nextPosition.Y <= Map.Height - Constants.MapTileSize && nextPosition.Y >= 0);

        public void Move(Vector change, GameObject subject) =>
            subject.Position.Add(change);
    }
}