using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Game.GameRoom
{
    // Waiting for players to join.
    public class GameStateWaiting : IGameState
    {
        public IGameContext Context { get; }

        public GameStateWaiting(IGameContext context) =>
            Context = context;

        public void WaitForPlayers()
        {
            throw new NotImplementedException();
        }

        public void SetGameReady()
        {
            throw new NotImplementedException();
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }

        public void EndGame()
        {
            throw new NotImplementedException();
        }
    }
}