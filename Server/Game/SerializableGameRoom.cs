using System.Runtime.Serialization;

namespace Server.Game
{
    [DataContract]
    public class SerializableGameRoom
    {
        [DataMember] string RoomId { get; set; }
        [DataMember] int JoinedPlayers { get; set; }
        [DataMember] int MaxPlayers { get; set; }

        public SerializableGameRoom(string roomId, int joinedPlayers, int maxPlayers)
        {
            RoomId = roomId;
            JoinedPlayers = joinedPlayers;
            MaxPlayers = maxPlayers;
        }
    }
}