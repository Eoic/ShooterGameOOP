using System.Diagnostics;
using System.Linq;

namespace Server.Game.GameRoomControl
{
    // Waiting for players to join.
    public class GameStateWaiting : IGameState
    {
        public IGameContext Context { get; }

        public GameStateWaiting(IGameContext context) =>
            Context = context;

        public void WaitForPlayers() =>
            Debug.WriteLine("Already waiting for players...");

        public void SetGameReady()
        {
            var teamACount = Context.Players.Count((kvp) => kvp.Value.Team == Constants.TeamA);
            var teamBCount = Context.Players.Count((kvp) => kvp.Value.Team == Constants.TeamB);

            if (teamACount > 0 && teamBCount > 0)
            {
                // TODO: Position layers and forbid to move on cooldown.
                Debug.WriteLine("[Waiting -> Ready]");
                Context.State = new GameStateReady(Context);
                Context.TimeTillStateChange = Constants.GameReadyTime * 1000 / (1000 / 60);
                Context.UpdateTimer(Constants.GameStarting, Constants.GameReadyTime);
            }
            else
            {
                Debug.WriteLine("[Waiting -> Waiting]");
                Context.TimeTillStateChange = Constants.GameWaitTime * 1000 / (1000 / 60);
                Context.UpdateTimer(Constants.WaitingForPlayers, Constants.GameWaitTime);
            }
        }

        public void StartGame() =>
            Debug.WriteLine("Invalid state change: [Waiting -> Running]");

        public void EndGame()
        {
            Debug.WriteLine("[Waiting -> Ended]");
            Context.State = new GameStateEnded(Context);
        }
    }
}