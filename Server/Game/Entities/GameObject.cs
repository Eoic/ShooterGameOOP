using System;

namespace Server.Game.Entities
{
    public class GameObject
    {
        public Guid Id { get; }
        public Vector Position { get; private set; }

        public GameObject()
        {
            Id = Guid.NewGuid();
            Position = new Vector(0, 0);
        }
    }
}