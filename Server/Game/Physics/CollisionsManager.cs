using Server.Game.Entities;

namespace Server.Game.Physics
{
    public class CollisionsManager : ICollisionsManager
    {
        private readonly ICollider _collider;

        public CollisionsManager(ICollider collider) =>
            _collider = collider;

        public bool ProcessMotion(long delta, GameObject subject)
        {
            var change = subject.Direction * Constants.DefaultSpeed * delta;
            var nextPosition = subject.Position + change;

            if (!_collider.IsPositionValid(nextPosition)) 
                return false;
            
            _collider.Move(change, subject);
            return true;

        }
    }
}