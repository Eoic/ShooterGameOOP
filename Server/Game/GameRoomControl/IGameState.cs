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
        #region Deprecated
        /*
        void WaitForPlayers();
        void SetGameReady();
        void StartGame();
        void EndGame();
        */
        #endregion
        void Tick();
        void EndGame();
    }
}
