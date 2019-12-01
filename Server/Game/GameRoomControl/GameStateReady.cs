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

        public void EndGame()
        {
            Debug.WriteLine("[Ready -> Ended]");
            Context.SetState(new GameStateEnded(Context));
        }
    }
}