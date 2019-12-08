using System;
using System.Runtime.Serialization;

namespace Server.Game.Entities
{
    [DataContract]
    public abstract class GameObject
    {
        public Guid Id { get; protected set; }
        public Vector Position { get; set; }
        public Vector Direction { get; set; }

        protected GameObject()
        {
            Position = new Vector();
            Direction = new Vector();
        }

        public abstract void Update(long delta);
    }
}