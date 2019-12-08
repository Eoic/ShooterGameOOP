using Server.Game.Entities;

namespace Server.Game.Physics
{
    public class PlayerCollider : Collider
    {
        public override sealed bool ProcessMotion(long delta, GameObject subject) =>
            base.ProcessMotion(delta, subject);

        public override bool IsNextPositionValid(Vector nextPosition) => 
            (nextPosition.X <= Map.Width - Constants.MapTileSize && nextPosition.X >= 0 && 
             nextPosition.Y <= Map.Height - Constants.MapTileSize && nextPosition.Y >= 0);

        public override void Move(Vector change, GameObject subject) =>
            subject.Position.Add(change);

        public override Vector GetNextPosition(GameObject subject, Vector change) =>
            subject.Position + change;

        public override Vector GetPositionChange(GameObject subject, long delta) =>
            subject.Direction * Constants.DefaultSpeed * delta;
    }
}