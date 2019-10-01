using System;

namespace Server.Game.Entities
{
    public class GameObject
    {
        public Guid Id { get; }
        public string Name { get; }
        public Vector Position { get; set; }

        public GameObject()
        {
            Id = Guid.NewGuid();
            Position = new Vector();
        }

        public GameObject(string name) : this()
        {
            Name = name;
        }
    }
}