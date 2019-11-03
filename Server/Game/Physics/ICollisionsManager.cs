using Server.Game.Entities;

namespace Server.Game.Physics
{
    public interface ICollisionsManager
    {
        bool ProcessMotion(long delta, GameObject subject);
    }
}