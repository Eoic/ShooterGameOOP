using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Game.Entities;

namespace Server.Game
{
    public class GameRoom
    {
        public Dictionary<Guid, Player> Players { get; } = new Dictionary<Guid, Player>();
        public Guid RoomId { get; } = Guid.NewGuid();

        public void AddPlayer(Player player)
        {
            Players.Add(player.Id, player);
        }

        public Player GetPlayer(Guid id) =>
            Players[id];

        public void RemovePlayer(Guid id) =>
            Players.Remove(id);
    }
}