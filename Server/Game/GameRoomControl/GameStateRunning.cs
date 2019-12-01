using System.Diagnostics;

namespace Server.Game.GameRoomControl
{
    // Game started
    public class GameStateRunning : IGameState
    {
        public GameContext Context { get; }

        public GameStateRunning(GameContext context) =>
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
            throw new NotImplementedException();
        }

        public void EndGame()
        {
            // TODO: 
        }
        */
        #endregion

        public void Tick()
        {
            if (Context.TimeTillStateChange > 0)
            {
                Context.TimeTillStateChange--;
                return;
            }

            Debug.WriteLine("[Running -> Ended]");
            Context.SetState(new GameStateEnded(Context));
        }
    }
}