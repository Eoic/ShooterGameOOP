using Server.Utilities;
using System.Diagnostics;

namespace Server.Game.GameRoomControl
{
    // Brief cooldown before starting
    public class GameStateReady : IGameState
    {
        public GameContext Context { get; }

        public GameStateReady(GameContext context) =>
            Context = context;

        #region Deprecated
        /*
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
        */
        #endregion

        public void Tick()
        {
            if (Context.TimeTillStateChange > 0)
            {
                Context.UpdateStateChangeTime();
                return;
            }

            Debug.WriteLine("[Ready -> Running]");
            Context.TimeTillStateChange = TimeConverter.SecondsToTicks(Constants.GameDurationTime);
            Context.SetState(new GameStateRunning(Context));
            Context.UpdateTimer(Constants.GameEnding, Constants.GameDurationTime);
        }
    }
}