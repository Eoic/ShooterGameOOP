using System;

namespace Server.Game.Entities
{
    public class GameObject
    {
        public Guid Id { get; protected set; }
        public string Name { get; }
        public Vector Position { get; set; }

        public GameObject()
        {
            Position = new Vector();
        }

        public GameObject(string name) : this()
        {
            Name = name;
        }
    }
}