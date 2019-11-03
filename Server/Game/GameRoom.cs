using System;
using System.Text;
using Server.Game.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Server.Game
{
    public class GameRoom
    {
        public Guid RoomId { get; } = Guid.NewGuid();
        public int TimeTillRoomUpdate = Constants.RoomUpdateInterval;
        public Dictionary<Guid, Player> Players { get; } = new Dictionary<Guid, Player>();

        public void AddPlayer(Player player)
        {
            if(Players.Count < Constants.MaxPlayerCount)
                Players.Add(player.Id, player);
        }

        public Player GetPlayer(Guid id) =>
            Players.ContainsKey(id) ? Players[id] : null;

        public void RemovePlayer(Guid id) =>
            Players.Remove(id);

        public bool IsFull() =>
            Players.Count >= Constants.MaxPlayerCount;

        public void ForceUpdate()
        {
            TimeTillRoomUpdate = 1;
        }

        // Update all players.
        public void Update(long delta)
        {
            foreach (var keyValuePair in Players)
                keyValuePair.Value.Update(delta);

            if (TimeTillRoomUpdate == 0)
                TimeTillRoomUpdate = Constants.RoomUpdateInterval;

            TimeTillRoomUpdate--;
        }

        public override string ToString()
        {
            var dividerWidth = 88;
            var index = 0;
            var builder = new StringBuilder();
            builder.AppendLine(new string('-', dividerWidth));
            builder.AppendFormat("| {0, -50} | {1, -31} |\n", "Room ID", "Size");
            builder.AppendLine(new string('-', dividerWidth));
            builder.AppendFormat("| {0, -50} | {1, -31} |\n", RoomId.ToString(), Players.Count + " / " + Constants.MaxPlayerCount);
            builder.AppendLine(new string('-', dividerWidth));
            builder.AppendFormat("| {0, -3} | {1, -44} | {2, -23} | {3, -5} |\n", "Nr.", "Player ID", "Position", "Team");
            builder.AppendLine(new string('-', dividerWidth));

            foreach (var keyValuePair in Players) {
                var player = keyValuePair.Value;
                builder.AppendLine(new string('-', dividerWidth));
                builder.AppendFormat("| {0, -3} | {1, -44} | {2, -23} | {3, -5} |\n", ++index, keyValuePair.Key, player.Position, player.Team);

                builder.AppendLine(new string('-', dividerWidth));
                builder.AppendFormat("| Bullets |\n");
                builder.AppendLine(new string('-', dividerWidth));

                keyValuePair.Value.Bullets.ForEach((bullet) => {
                    if (bullet.IsActive)
                        builder.AppendLine(bullet.ToString());
                });
            }
            
            builder.AppendLine(new string('-', dividerWidth));
            return builder.ToString();
        }
        
        public SerializableGameRoom GetSerializable()
        {
            return new SerializableGameRoom(RoomId.ToString(), Players.Count, Constants.MaxPlayerCount);
        }

        [DataContract]
        public class SerializableGameRoomId
        {
            [DataMember]
            public Guid RoomId { get; set; }
            [DataMember]
            public int Team { get; set; }

            public SerializableGameRoomId(string roomId, int team)
            {
                RoomId = Guid.Parse(roomId);
                Team = team;
            }
        }
    }
}