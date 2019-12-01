using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.GameRoomControl
{
    public interface IGameState
    {
        GameContext Context { get; }
        void Tick();
        void EndGame();
    }
}
