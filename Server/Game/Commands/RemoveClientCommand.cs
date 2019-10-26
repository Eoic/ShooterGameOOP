using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Server.Game.Commands
{
    public class RemoveClientCommand : ICommand
    {
        private Guid clientId;
        private Guid lastRemovedPlayer;
        private List<GameRoom> _games;

        public RemoveClientCommand(Guid clientId, List<GameRoom> games)
        {
            this.clientId = clientId;
            _games = games;
        }

        public void Execute()
        {
            lastRemovedPlayer = clientId;
            PlayerManager.RemovePlayer(_games, clientId);
        }

        public void Undo()
        {
            PlayerManager.AddPlayer(_games, lastRemovedPlayer);
        }
    }
}