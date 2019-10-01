﻿using System;
using Microsoft.Web.WebSockets;
using System.Collections.Generic;
using System.Diagnostics;
using Server.Utilities;

namespace Server.Network
{
    public class GameWebSocketHandler : WebSocketHandler, ISubject<Message>
    {
        public readonly Guid Id = Guid.NewGuid();
        private readonly List<IObserver<Message>> _observers = new List<IObserver<Message>>();

        /// <summary>
        /// Notifies observers when connection with
        /// client has been opened.
        /// </summary>
        public override void OnOpen()
        {
            ConnectionsPool.GetInstance().Clients.Add(this);
            var count = ConnectionsPool.GetInstance().Clients.Count;
            NotifyAllObservers(new Message(EventType.ClientConnected, $"New client connected. Current count: {count}."));
        }

        /// <summary>
        /// Passes received message to all observers.
        /// </summary>
        /// <param name="message"></param>
        public override void OnMessage(string message)
        {
            var messageObj = JsonParser.Deserialize<Message>(message);

            if (messageObj == null || messageObj.Type == EventType.Invalid)
            {
                Debug.WriteLine("Malformed message.");
                return;
            }
            
            NotifyAllObservers(messageObj);
        }
        
        /// <summary>
        /// Notifies all observers when connection with client
        /// is lost and removes client instance from connections list.
        /// </summary>
        public override void OnClose()
        {
            ConnectionsPool.GetInstance().Clients.Remove(this);
            var count = ConnectionsPool.GetInstance().Clients.Count;
            NotifyAllObservers(new Message(EventType.ClientDisconnected, $"Client has disconnected. Current count: {count}."));
        }

        /// <summary>
        /// Notifies all observers on connection error with client
        /// and removes client instance from connections list.
        /// </summary>
        public override void OnError()
        {
            ConnectionsPool.GetInstance().Clients.Remove(this);
            NotifyAllObservers(new Message(EventType.ErrorOccured, "An error occured"));
        }

        /// <summary>
        /// Adds given observer to observers list.
        /// </summary>
        /// <param name="observer"></param>
        public void Attach(IObserver<Message> observer) =>
            _observers.Add(observer);

        /// <summary>
        /// Removes given observer from observers list.
        /// </summary>
        /// <param name="observer"></param>
        public void Detach(IObserver<Message> observer) =>
            _observers.Remove(observer);

        /// <summary>
        /// Sends data to all observers.
        /// </summary>
        /// <param name="data"></param>
        public void NotifyAllObservers(Message data) => 
            _observers.ForEach(observer => observer.Update(data));
    }
}