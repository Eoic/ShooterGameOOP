namespace Server.Network
{
    // Event types received from client 
    // through web socket connection
    public enum MessageType
    {
        CLIENT_DISCONNECTED,
        CLIENT_CONNECTED,
        RECEIVED_MESSAGE,
        START_GAME,
        END_GAME,
        ERROR,
    }
}