using System;
using System.Collections.Generic;

namespace Server.Game.Commands
{
    public class AddPlayerCommand : ICommand
    {
        private Guid clientId;
        private Guid lastAddedPlayer;
        private List<GameRoom> _games;

        public AddPlayerCommand(Guid clientId, List<GameRoom> games)
        {
            this.clientId = clientId;
            _games = games;
        }

        public void Execute()
        {
            lastAddedPlayer = clientId;
            PlayerManager.AddPlayer(_games, clientId);
        }

        public void Undo()
        {
            PlayerManager.RemovePlayer(_games, lastAddedPlayer);
        }
    }
}