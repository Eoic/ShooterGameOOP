using Server.Network;
using Server.Utilities;
using System.Diagnostics;
using System.Collections.Generic;
using Server.Game.Entities;

namespace Server.Game.GameRoomControl
{
    // Game ended
    public class GameStateEnded : IGameState
    {
        public GameContext Context { get; }
        private bool _wasInvoked;

        public GameStateEnded(GameContext context) =>
            Context = context;

        public void Tick()
        {
            if (!_wasInvoked)
            {
                Debug.WriteLine("[Ended]");
                List<Player.PlayerState> gameResults = new List<Player.PlayerState>();
                
                // Collect game results
                foreach (var player in Context.Players)
                    gameResults.Add(player.Value.GetState());

                // Send results to players
                foreach (var player in Context.Players)
                {
                    var client = ConnectionsPool.GetInstance().GetClient(player.Key);
                    var gameEndData = new Message(ResponseCode.GameEnded, JsonParser.Serialize(gameResults));
                    client.Send(JsonParser.Serialize(gameEndData));
                }

                _wasInvoked = true;
                Context.GameEnded = true;
            }
        }

        public void EndGame() =>
            throw new System.InvalidOperationException("Invalid state change [Ended -> Ended]");
    }
}