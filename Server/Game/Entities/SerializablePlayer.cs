using Server.Models;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Server.Game.Entities
{
    [DataContract]
    public class SerializablePlayer
    {
        [DataMember] public string PlayerId { get; set; }
        [DataMember] public Vector Position { get; set; }
        [DataMember] public Vector Direction { get; set; }
        [DataMember] public int Type { get; set; }
        [DataMember] public int Team { get; set; }
        [DataMember] public List<Bullet.SerializableBullet> Bullets { get; set; }

        public SerializablePlayer(Vector position, Vector direction, int type, string playerId, int team, List<Bullet.SerializableBullet> bullets)
        {
            Position = position;
            Direction = direction;
            Type = type;
            PlayerId = playerId;
            Team = team;
            Bullets = bullets;
        }
    }
}