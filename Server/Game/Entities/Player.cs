using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Server.Game.Physics;
using Server.Models.GunFactory;
using System.Runtime.Serialization;

namespace Server.Game.Entities
{
    public class Player : GameObject
    {
        public Guid RoomId { get; set; }
        public int Health { get; private set; }
        public int Speed { get; set; }
        public Weapon Weapon { get; set; }
        public int Team { get; private set; }
        public List<Bullet> Bullets { get; set; }
        public CollisionsManager PlayerCollisionsManager { get; }
        public bool IsAlive { get => (Health > 0); }
        public string Name { get => "PLAYER_" + Id.ToString().Substring(0, 5); }

        public Player() { }

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
        public void AddBullet(Vector position, Vector direction, Network.IObserver<string> hitsObserver)
        {
            if (Weapon.Ammo > 0)
            {
                Weapon.DepleteAmmo();

                foreach (var bullet in Bullets.Where(bullet => bullet.IsActive == false))
                {
                    bullet.SetPosition(position);
                    bullet.SetDirection(direction);
                    bullet.IsActive = true;
                    bullet.Attach(hitsObserver);
                    break;
                }
            }
        }

        // Create bullet pool with primary weapon
        private void CreateBulletPool()
        {
            var pistolFactory = new PistolFactory();
            Weapon = pistolFactory.CreateWeapon();

            for (var i = 0; i < Constants.DefaultBulletPoolSize; i++)
                Bullets.Add(pistolFactory.CreateBullet());
        }

        public SerializablePlayer GetSerializable() =>
            new SerializablePlayer(Position, Direction, 0, Id.ToString(), Team, Health, new List<Bullet.SerializableBullet>());

        // Return all active bullets for serialization
        public List<Bullet.SerializableBullet> GetBullets() =>
            (from activeBullet in Bullets where activeBullet.IsActive select activeBullet.GetSerializable()).ToList();

        public override void Update(long delta)
        {
            // Update player position.
            PlayerCollisionsManager.ProcessMotion(delta, this);

            // Update bullets
            foreach (var bullet in Bullets.Where(bullet => bullet.IsActive))
                bullet.Update(delta);
        }


        public PlayerState GetState()
        {
            var name = "PLAYER_" + Id.ToString().Substring(0, 5);
            return new PlayerState(name, Team, Health);
        }

        [DataContract]
        public class PlayerState
        {
            [DataMember]
            public string Name { get; set; }
            
            [DataMember]
            public int Team { get; set; }
            
            [DataMember]
            public int Health { get; set; }

            public PlayerState(string name, int team, int health)
            {
                Name = name;
                Team = team;
                Health = health;
            }
        }
    }
}