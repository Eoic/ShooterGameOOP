using Server.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Server.Game.Physics;

namespace Server.Game.Entities
{
    public class Player : GameObject
    {
        public Guid RoomId { get; set; }
        public int Health { get; private set; }
        public int Speed { get; set; }
        public int Team { get; private set; }
        public List<Bullet> Bullets { get; set; }
        public CollisionsManager PlayerCollisionsManager { get; }
        public Player(Guid id, Guid roomId)
        {
            Id = id;
            RoomId = roomId;
            Speed = Constants.DefaultSpeed;
            Direction = new Vector(0, 0);
            Health = Constants.MaxHealth;
            Bullets = new List<Bullet>();
            PlayerCollisionsManager = new CollisionsManager(new PlayerCollider());
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
            // Update player position.
            PlayerCollisionsManager.ProcessMotion(delta, this);

            // Update bullets
            foreach (var bullet in Bullets.Where(bullet => bullet.IsActive))
                bullet.Update(delta);
        }
    }
}