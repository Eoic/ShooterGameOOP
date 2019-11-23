using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.GameRoomControl
{
    public interface IGameState
    {
        IGameContext Context { get; }
        void WaitForPlayers();
        void SetGameReady();
        void StartGame();
        void EndGame();
    }
}
