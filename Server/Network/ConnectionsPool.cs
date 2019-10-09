using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.Web.WebSockets;

namespace Server.Network
{
    /// <summary>
    /// Holds all client connections.
    /// </summary>
    public class ConnectionsPool
    {
        private static readonly object InstanceLock = new object();
        public readonly WebSocketCollection Clients = new WebSocketCollection();
        private static ConnectionsPool _instance;

        public static ConnectionsPool GetInstance()
        {
            lock (InstanceLock)
            {
                return _instance ?? (_instance = new ConnectionsPool());
            }
        }

        public Client GetClient(Guid id) =>
            (Client) Clients.SingleOrDefault(client => ((Client) client).Id == id);

        public void RemoveClient(Guid id)
        {
            var client = GetClient(id);

            if (client != null)
                Clients.Remove(client);
        }
    }
}