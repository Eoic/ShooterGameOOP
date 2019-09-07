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

            server.OnOpen += (object sender, EventArgs arguments) =>
            {
                Console.WriteLine("Server is running.");
            };

            server.OnConnection += (object sender, EventArgs arguments) =>
            {
                WriteLine("Client connected");
            };

            server.Listen();
            Console.ReadLine();
        }
    }
}
