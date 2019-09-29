using Microsoft.Web.WebSockets;

// For holding all client connections.
namespace Server.Network
{
    public class ConnectionsPool
    {
        private static ConnectionsPool instance;
        public readonly WebSocketCollection Clients;

        private ConnectionsPool()
        {
            Clients = new WebSocketCollection();
        }

        public static ConnectionsPool GetInstance()
        {
            if (instance == null)
                instance = new ConnectionsPool();
            return instance;
        }
    }
}