using System;
using System.Linq;
using Microsoft.Web.WebSockets;

namespace Server.Network
{
    /// <summary>
    /// Holds all client connections.
    /// </summary>
    public class ConnectionsPool
    {
        public readonly WebSocketCollection Clients = new WebSocketCollection();
        private static ConnectionsPool _instance;

        public static ConnectionsPool GetInstance() =>
            _instance ?? (_instance = new ConnectionsPool());

        public Client GetClient(Guid id) =>
            (Client) Clients.SingleOrDefault(client => ((Client) client).Id == id);

        public void RemoveClient(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}