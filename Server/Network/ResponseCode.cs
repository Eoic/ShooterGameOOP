﻿using System;
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
            BonusesCreated = 5;
    }
}