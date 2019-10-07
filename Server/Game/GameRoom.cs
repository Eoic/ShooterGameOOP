using System;
using System.Collections.Generic;
using System.Diagnostics;
using Server.Game.Entities;

namespace Server.Game
{
    public class GameRoom
    {
        public Guid RoomId { get; } = Guid.NewGuid();
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
    }
}