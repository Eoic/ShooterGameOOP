using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Dynamic;

namespace Server.Core
{
    internal class WebSocketServer
    {
        private static Socket _socketServer;
        private static List<Socket> _clients;
        private static byte[] _buffer;
        private readonly int _backlog;
        private readonly int _bufferSize;

        // Events
        public event EventHandler<ServerEventArgs> OnOpen;
        public event EventHandler<ServerEventArgs> OnClose;
        public event EventHandler<ServerEventArgs> OnConnection;
        public event EventHandler<ServerEventArgs> OnMessage;

        public WebSocketServer(int backlog = ServerConstants.Backlog, int bufferSize = ServerConstants.BufferSize)
        {
            _socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _clients = new List<Socket>();
            _bufferSize = bufferSize;
            _buffer = new byte[_bufferSize];
            _backlog = backlog;
        }

        // Listen on given or default port.
        public void Listen(int port = ServerConstants.Port)
        {
            _socketServer.Bind(new IPEndPoint(IPAddress.Any, port));
            _socketServer.Listen(_backlog);
            _socketServer.BeginAccept(AcceptCallback, null);
            dynamic args = new ExpandoObject();
            args.Port = port;
            Open(new ServerEventArgs(args));
        }

        // Accept client connection.
        private void AcceptCallback(IAsyncResult result)
        {
            var socket = _socketServer.EndAccept(result);
            _clients.Add(socket);
            socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, socket);
            _socketServer.BeginAccept(AcceptCallback, null);
            Connection(ServerEventArgs.Empty);
        }

        // Receive data.
        private void ReceiveCallback(IAsyncResult result)
        {
            var socket = (Socket)result.AsyncState;
            var received = socket.EndReceive(result);
            var dataBuffer = new byte[received];
            Array.Copy(_buffer, dataBuffer, received);
            var message = Encoding.ASCII.GetString(dataBuffer);
            dynamic args = new ExpandoObject();
            args.Message = message;
            args.Client = socket;
            Message(new ServerEventArgs(args));
        }

        // Send data.
        public void SendData(string data, Socket client)
        {
            var dataBytes = Encoding.ASCII.GetBytes(data);
            client.BeginSend(dataBytes, 0, dataBytes.Length, SocketFlags.None, SendCallback, client);
            client.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, ReceiveCallback, client);
        }

        private static void SendCallback(IAsyncResult result)
        {
            var socket = (Socket)result.AsyncState;
            socket.EndSend(result);
        }

        // Events.
        private void Open(ServerEventArgs arguments) => InvokeEvent(OnOpen, arguments);
        private void Close(ServerEventArgs arguments) => InvokeEvent(OnClose, arguments);
        private void Connection(ServerEventArgs arguments) => InvokeEvent(OnConnection, arguments);
        private void Message(ServerEventArgs arguments) => InvokeEvent(OnMessage, arguments);

        private void InvokeEvent(EventHandler<ServerEventArgs> serverEvent, ServerEventArgs arguments) =>
            serverEvent?.Invoke(this, arguments);
    }
}