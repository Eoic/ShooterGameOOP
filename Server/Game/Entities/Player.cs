using Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Server.Game.Entities
{
    public class Player : GameObject
    {
        public Guid RoomId { get; set; }
        public int Health { get; private set; }
        public Vector Direction { get; set; }
        public int Speed { get; set; }
        public int Team { get; private set; }
        public List<Bullet> Bullets { get; set; } 

        public Player(Guid id, Guid roomId)
        {
            Id = id;
            RoomId = roomId;
            Speed = Constants.DefaultSpeed;
            Direction = new Vector(0, 0);
            Health = Constants.MaxHealth;
            Bullets = new List<Bullet>();
            CreateBulletPool();
        }

        public void TakeDamage(int damage)
        {
            Health -= damage;

            if (Health < Constants.MinHealth)
                Health = Constants.MinHealth;
        }

        public void TakeBonus()
        {
            Health += Constants.BonusHealth;

            if (Health >= Constants.MaxHealth)
                Health = Constants.MaxHealth;
        }

        public void JoinTeam(int teamId)
        {
            Team = teamId;
        }

        // Allocate new bullet from bullet pool
        public void AddBullet(Vector position, Vector direction)
        {
            for (var i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i].IsActive == false)
                {
                    Bullets[i].SetPosition(position);
                    Bullets[i].SetDirection(direction);
                    Bullets[i].IsActive = true;
                    break;
                }
            }
        }

        private void CreateBulletPool()
        {
            for (var i = 0; i < Constants.DefaultBulletPoolSize; i++)
                Bullets.Add(new Bullet(1, "where"));
        }

        // Return all active bullets for serialization
        public List<Bullet.SerializableBullet> GetBullets()
        {
            var bullets = new List<Bullet.SerializableBullet>();

            foreach (var activeBullet in Bullets)
                if (activeBullet.IsActive)
                    bullets.Add(activeBullet.GetSerializable());

            return bullets;
        }

        public override void Update(long delta)
        {
            var change = Direction * Speed * delta;
            var newPosition = Position + change;

            // Update player position
            if (newPosition.X <= Map.Width - Constants.MapTileSize && newPosition.X >= 0 && newPosition.Y <= Map.Height - Constants.MapTileSize && newPosition.Y >= 0)
                Position.Add(change);

            // Update its bullets
            for (var i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i].IsActive == false)
                    continue;

                var position = Bullets[i].Position;
                var xPos = position.X;
                var yPos = position.Y;

                if (xPos < -Constants.MapTileHalfSize ||
                    xPos > Constants.MapWidth * Constants.MapTileSize - Constants.MapTileHalfSize ||
                    yPos < -Constants.MapTileHalfSize ||
                    yPos > Constants.MapHeight * Constants.MapTileSize - Constants.MapTileHalfSize)
                {
                    Bullets[i].IsActive = false;
                    continue;
                }

                Bullets[i].Update(delta);
            }
        }
    }
}