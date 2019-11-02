using System;
using System.Collections.Generic;

namespace Server.Game.Commands
{
    public class AddPlayerCommand : ICommand
    {
        private Guid clientId;
        private Guid roomId;
        private Guid lastAddedPlayer;
        private int team;
        private List<GameRoom> _games;

        public AddPlayerCommand(Guid clientId, Guid roomId, int team, List<GameRoom> games)
        {
            this.clientId = clientId;
            this.roomId = roomId;
            this.team = team;
            _games = games;
        }

        public void Execute()
        {
            lastAddedPlayer = clientId;
            PlayerManager.AddPlayer(_games, clientId, roomId, team);
        }

        public void Undo()
        {
            PlayerManager.RemovePlayer(_games, lastAddedPlayer, roomId);
        }
    }
}