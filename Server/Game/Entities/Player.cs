using System;

namespace Server.Game.Entities
{
    public class Player : GameObject
    {
        public Player(Guid id) =>
            Id = id;

        public int Health { get; private set; }
        public bool IsMoving { get; set; }
        public Vector Velocity { get; set; }
        public int Speed { get; set; }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health < 0)
                Health = 0;
        }
    }
}