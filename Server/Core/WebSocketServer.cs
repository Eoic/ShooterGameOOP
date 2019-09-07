using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using static System.Console;

namespace Server.Core
{
    class WebSocketServer
    {
        private static Socket _socketServer;
        private static List<Socket> _clients;
        private static byte[] _buffer;
        private int _backlog;
        private int _bufferSize;

        // Events
        public event EventHandler OnOpen;
        public event EventHandler OnClose;
        public event EventHandler OnConnection;
        public event EventHandler OnMessage;

        public WebSocketServer(int backlog = ServerConstants.Backlog, int bufferSize = ServerConstants.BufferSize)
        {
            _socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<Socket>();
            _buffer = new byte[bufferSize];
            _bufferSize = bufferSize;
            _backlog = backlog;
        }

        // Listen ongiven or default port.
        public void Listen(int port = ServerConstants.Port)
        {
            _socketServer.Bind(new IPEndPoint(IPAddress.Any, port));
            _socketServer.Listen(_backlog);
            _socketServer.BeginAccept(new AsyncCallback(AcceptCallback), null);
            Open();
        }

        // Accept client connection.
        private void AcceptCallback(IAsyncResult result)
        {
            var socket = _socketServer.EndAccept(result);
            _clients.Add(socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            _socketServer.BeginAccept(new AsyncCallback(AcceptCallback), null);
            Connection();
        }

        // Receive data.
        private static void ReceiveCallback(IAsyncResult result)
        {
            var socket = (Socket)result.AsyncState;
            var received = socket.EndReceive(result);
            var dataBuffer = new byte[received];
            Array.Copy(_buffer, dataBuffer, received);
            var text = Encoding.ASCII.GetString(dataBuffer);
            Console.WriteLine("Received: " + text);

            var response = string.Empty;

            if (text.ToLower() != "get time")
            {
                response = "Invalid request";
            }
            else
            {
                response = DateTime.Now.ToLongTimeString();
            }

            var data = Encoding.ASCII.GetBytes(response);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }

        // Send data.
        private static void SendCallback(IAsyncResult result)
        {
            var socket = (Socket)result.AsyncState;
            socket.EndSend(result);
        }

        // Events.
        private void Open() => InvokeEvent(OnOpen);
        private void Close() => InvokeEvent(OnClose);
        private void Connection() => InvokeEvent(OnConnection);
        private void Message() => InvokeEvent(OnMessage);

        private void InvokeEvent(EventHandler serverEvent)
        {
            if (serverEvent != null)
                serverEvent(this, EventArgs.Empty);
        }
    }
}