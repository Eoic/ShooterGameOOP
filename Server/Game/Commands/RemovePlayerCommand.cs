using System;
using System.Collections.Generic;

namespace Server.Game.Commands
{
    public class RemovePlayerCommand : ICommand
    {
        private Guid clientId;
        private Guid roomId;
        private Guid lastRemovedPlayer;
        private List<GameRoom> _games;

        public RemovePlayerCommand(Guid clientId, Guid roomId, List<GameRoom> games)
        {
            this.clientId = clientId;
            this.roomId = roomId;
            _games = games;
        }

        public void Execute()
        {
            lastRemovedPlayer = clientId;
            PlayerManager.RemovePlayer(_games, clientId, roomId);
        }

        public void Undo()
        {
            PlayerManager.AddPlayer(_games, lastRemovedPlayer, roomId);
        }
    }
}