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
                Console.WriteLine($"Server is running on port {arguments.Data.Port}.");
            };

            server.OnConnection += (object sender, ServerEventArgs arguments) =>
            {
                WriteLine("Client connected");
            };

            server.Listen();
            Console.ReadLine();
        }
    }
}
