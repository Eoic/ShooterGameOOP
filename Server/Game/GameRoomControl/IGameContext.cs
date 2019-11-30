using Server.Game.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.GameRoomControl
{
    public interface IGameContext
    {
        public Dictionary<Guid, Player> Players { get; }
        public IGameState State { get; set; }
        public int TimeTillStateChange { get; set; }
        void UpdateTimer(string label, int value);
    }
}
