using Server.Utilities;
using System.Diagnostics;
using System.Linq;

namespace Server.Game.GameRoomControl
{
    // Waiting for players to join.
    public class GameStateWaiting : IGameState
    {
        public GameContext Context { get; }

        public GameStateWaiting(GameContext context) =>
            Context = context;

        public void Tick()
        {
            if (Context.TimeTillStateChange > 0)
            {
                Context.UpdateStateChangeTime();
                return;
            }

            var teamACount = Context.Players.Count((kvp) => kvp.Value.Team == Constants.TeamA);
            var teamBCount = Context.Players.Count((kvp) => kvp.Value.Team == Constants.TeamB);

            if (teamACount > 0 && teamBCount > 0)
            {
                // TODO: Position players and forbid to move on cooldown.
                Debug.WriteLine("[Waiting -> Ready]");
                Context.TimeTillStateChange = TimeConverter.SecondsToTicks(Constants.GameReadyTime);
                Context.SetState(new GameStateReady(Context));
                Context.UpdateTimer(Constants.GameStarting, Constants.GameReadyTime);
            }
            else
            {
                Debug.WriteLine("[Waiting -> Waiting]");
                Context.TimeTillStateChange = TimeConverter.SecondsToTicks(Constants.GameWaitTime);
                Context.UpdateTimer(Constants.WaitingForPlayers, Constants.GameWaitTime);
            }
        }

        public void EndGame()
        {
            Debug.WriteLine("[Waiting -> Ended]");
            throw new System.NotImplementedException();
        }
    }
}