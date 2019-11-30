using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Game.GameRoomControl
{
    // Brief cooldown before starting
    public class GameStateReady : IGameState
    {
        public IGameContext Context { get; }

        public GameStateReady(IGameContext context) =>
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
            Context.TimeTillStateChange = Constants.GameDurationTime * 1000 / (1000 / 60);
            Context.UpdateTimer(Constants.GameEnding, Constants.GameDurationTime);
        }

        public void EndGame()
        {
            throw new NotImplementedException();
        }
    }
}