using System;
using Server.Core;
using static System.Console;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new WebSocketServer();

            server.OnOpen += (object sender, ServerEventArgs arguments) =>
            {
                WriteLine($"Server is running on port {arguments.Data.Port}.");
            };

            server.OnConnection += (object sender, ServerEventArgs arguments) =>
            {
                WriteLine("Client connected.");
            };

            server.OnMessage += (object sender, ServerEventArgs arguments) =>
            {
                WriteLine($"Received: {arguments.Data.Message}");
                // server.SendData("hi", arguments.Data.Client);
            };

            server.OnClose += (object sender, ServerEventArgs arguments) =>
            {
                WriteLine("Client disconnected.");
            };

            server.Listen();
            Console.ReadLine();
        }
    }
}
