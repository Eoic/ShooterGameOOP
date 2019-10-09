using System.Runtime.Serialization;

namespace Server.Game.Entities
{
    [DataContract]
    public class SerializablePlayer
    {
        [DataMember] public Vector Position { get; set; }
        [DataMember] public Vector Direction { get; set; }
        [DataMember] public int Type { get; set; }

        public SerializablePlayer(Vector position, Vector direction, int type)
        {
            Position = position;
            Direction = direction;
            Type = type;
        }
    }
}