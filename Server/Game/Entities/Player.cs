using System;

namespace Server.Game.Entities
{
    public class Player : GameObject
    {
        public Guid RoomId { get; set; }
        public int Health { get; private set; }
        public bool ShouldUpdate => Math.Abs(Direction.X) < 0.01 && Math.Abs(Direction.Y) < 0.01;
        public Vector Direction { get; set; }
        public int Speed { get; set; }
        public int TimeTillClientUpdate = Constants.PlayerUpdateInterval;

        public Player(Guid id, Guid roomId)
        {
            Id = id;
            RoomId = roomId;
            Speed = Constants.DefaultSpeed;
            Direction = new Vector(0, 0);
            Health = Constants.MaxHP;
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health < Constants.MinHP)
                Health = Constants.MinHP;
        }

        public void TakeBonus()
        {
            Health += Constants.BonusHP;

            if (Health >= Constants.MaxHP)
                Health = Constants.MaxHP;
        }

        public override void Update(long delta)
        {
            Position.Add(Direction * Speed * delta);

            if (TimeTillClientUpdate == 0)
                TimeTillClientUpdate = Constants.PlayerUpdateInterval;

            TimeTillClientUpdate--;
        }
    }
}