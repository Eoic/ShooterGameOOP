namespace Server.Network
{
    /// <summary>
    /// Event types received from client 
    /// through web socket connection
    /// </summary>
    public enum EventType
    {
        // Events
        Invalid, // If serialization goes wrong
        ClientConnected,
        ClientDisconnected,
        ErrorOccured,
        
        // Client request types
        CreateGame,
        StartGame,
        EndGame,
    }
}