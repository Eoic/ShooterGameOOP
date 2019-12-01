using System;
using Server.Game.Entities;
using System.Collections.Generic;

namespace Server.Game.GameRoomControl
{
    public abstract class GameContext
    {
        public IGameState State { get; protected set; }
        public int TimeTillStateChange { get; set; }
        public Dictionary<Guid, Player> Players { get; protected set; }
        public abstract void UpdateTimer(string label, int value);
        public abstract void UpdateStateChangeTime();
        public abstract void SetPlayersReady();
        public abstract void SetState(IGameState state);
    }
}
