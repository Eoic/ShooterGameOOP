namespace Server.Network
{
    /// <summary>
    /// Event types received from client 
    /// through web socket connection
    /// </summary>
    public enum EventType
    {
        // SERVER EVENTS
        Invalid,
        ClientConnected,
        ClientDisconnected,
        ErrorOccured,
        
        // CLIENT REQUEST TYPES
        // Game session
        CreateGame,
        StartGame,
        EndGame,

        // Actions
        StartMoving,
        StopMoving,
        Shoot,
    }
}