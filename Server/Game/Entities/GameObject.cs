using System;

namespace Server.Game.Entities
{
    public abstract class GameObject
    {
        public Guid Id { get; protected set; }
        public Vector Position { get; set; }

        public GameObject()
        {
            Position = new Vector();
        }

        public abstract void Update(long delta);
    }
}