using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Network
{
    /// <summary>
    /// Response code sent to the client.
    /// </summary>
    public static class ResponseCode
    {
        public const int
            // Does nothing
            Ping = 0,

            // Notify about created game
            GameCreated = 1,

            // Notify about joined game
            GameJoined = 2,

            // Notify about quit game
            GameQuit = 3,

            // Notify about updated position
            PositionUpdated = 4,

            // Notify about created bonuses
            BonusesCreated = 5,

            // Sending game list
            GameListFormed = 6,

            // Player joined client's room
            NewPlayerJoined = 7,

            // New timer value.
            NewTimerValue = 8,

            // After game match ends.
            GameEnded = 9,

            // Player has 0 health left.
            PlayerDied = 10;
    }
}