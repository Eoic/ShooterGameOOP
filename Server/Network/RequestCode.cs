namespace Server.Network
{
    /// <summary>
    /// Request code received from the client.
    /// </summary>
    public static class RequestCode
    {
        public const int
            // Does nothing.
            Ping = 0,

            // Client asks to connect
            Connect = 1,

            // Client asks to disconnect
            Disconnect = 2,

            // Notifies about the error
            RaiseError = 3,

            // Creates new game
            CreateGame = 4,

            // Joins an existing game
            JoinGame = 5,

            // Quits game
            QuitGame = 6,

            // Updates movement direction
            UpdateDirection = 7,

            // Notifies about shot fired
            Shoot = 8,

            // List of joinable games
            FormGameList = 9;
    }
}