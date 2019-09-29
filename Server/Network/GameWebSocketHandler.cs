using Microsoft.Web.WebSockets;
using System.Collections.Generic;

namespace Server.Network
{
    public class GameWebSocketHandler : WebSocketHandler, ISubject<Message>
    {
        private readonly List<IObserver<Message>> observers = new List<IObserver<Message>>();

        // Add new client to client connections list
        public override void OnOpen()
        {
            ConnectionsPool.GetInstance().Clients.Add(this);
            int count = ConnectionsPool.GetInstance().Clients.Count;
            NotifyAllObservers(new Message(MessageType.CLIENT_CONNECTED, $"New client connected. Current count: {count}."));
        }

        // Called on message received from client
        public override void OnMessage(string message)
        {
            NotifyAllObservers(new Message(MessageType.RECEIVED_MESSAGE, message));
        }

        // Called if client disconnects
        public override void OnClose()
        {
            ConnectionsPool.GetInstance().Clients.Remove(this);
            int count = ConnectionsPool.GetInstance().Clients.Count;
            NotifyAllObservers(new Message(MessageType.CLIENT_DISCONNECTED, $"Client has disconnected. Current count: {count}."));
        }

        // An error occoured
        public override void OnError()
        {
            NotifyAllObservers(new Message(MessageType.ERROR, "An error occoured"));
        }

        // Attach an observer to this client
        public void Attach(IObserver<Message> observer) =>
            observers.Add(observer);

        // Detach an observer from this client
        public void Detach(IObserver<Message> observer) =>
            observers.Remove(observer);

        // Send messages to all attached observers
        public void NotifyAllObservers(Message data) => 
            observers.ForEach(observer => observer.Update(data));
    }
}