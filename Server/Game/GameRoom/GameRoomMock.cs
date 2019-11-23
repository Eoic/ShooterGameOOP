using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Game.GameRoom
{
    public class GameRoomMock : IGameContext
    {
        public IGameState State { get; }

        public GameRoomMock()
        {
            State = new GameStateWaiting(this);
        }

        public void StartGame()
        {
            State.StartGame();
        }

        public void EndGame()
        {
            State.EndGame();
        }

        public void SetGameReady()
        {
            State.SetGameReady();
        }

        public void WaitForPlayers()
        {
            State.WaitForPlayers();
        }
    }
}