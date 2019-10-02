namespace Server.Network
{
    /// <summary>
    /// Events received from client 
    /// through web socket connection
    /// In short: What is received from client
    /// </summary>
    public static class EventType
    {
        // SERVER EVENTS
        public const int
            Invalid = 0,
            ClientConnected = 1,
            ClientDisconnected = 2,
            ErrorOccured = 3,

            // CLIENT REQUEST TYPES
            // Game session
            CreateGame = 4,
            StartGame = 5,
            EndGame = 6,

            // Actions
            DirectionUpdate = 8,
            Shoot = 9,

            // RESPONSE TO CLIENT
            GameCreated = 10,
            PositionUpdate = 11;
    }
}