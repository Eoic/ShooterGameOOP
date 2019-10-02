using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Game.Entities;

namespace Server.Game
{
    public class GameRoom
    {
        public Guid RoomId { get; } = Guid.NewGuid();
        private readonly Dictionary<Guid, Player> _players = new Dictionary<Guid, Player>();

        public void AddPlayer(Player player)
        {
            _players.Add(player.Id, player);
        }

        public Player GetPlayer(Guid id)
        {
            return _players[id];
        }
    }
}