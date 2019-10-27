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

        public SerializablePlayer(Vector position, Vector direction, int type, string playerId)
        {
            Position = position;
            Direction = direction;
            Type = type;
            PlayerId = playerId;
        }
    }
}