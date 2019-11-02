using Server.Game;
using Server.Game.Entities;
using System.Runtime.Serialization;
using System.Text;

namespace Server.Models
{
    public class Bullet : GameObject
    {
        public int Id { get; set; }
        public double Damage { get; set; }
        public string GunType { get; set; }
        public Vector Direction { get; set; }
        public bool IsActive { get; set; }

        public Bullet(double damage, string gunType)
        {
            Damage = damage;
            GunType = gunType;
            Position = new Vector(0, 0);
            Direction = new Vector(0, 0);
        }

        public void SetPosition(Vector position)
        {
            Position = new Vector(position.X, position.Y);
        }

        public void SetDirection(Vector direction)
        {
            Direction = new Vector(direction.X, direction.Y);
        }

        public override void Update(long delta)
        {
            var change = Direction * delta * Constants.DefaultBulletSpeed;
            Position.Add(change);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendFormat("| {0, -30} | {1, -15} |", Position.ToString(), Direction.ToString());
            return builder.ToString();
        }
        
        public SerializableBullet GetSerializable()
        {
            return new SerializableBullet(Id, Position, Direction);
        }

        [DataContract]
        public class SerializableBullet
        {
            [DataMember]
            public int Id { get; set; }
            [DataMember]
            public Vector Position { get; set; }
            [DataMember]
            public Vector Direction { get; set; }

            public SerializableBullet(int id, Vector position, Vector direction)
            {
                Id = id;
                Position = position;
                Direction = direction;
            }
        }
    }
}