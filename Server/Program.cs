using Server.Core;
using static System.Console;

namespace Server
{
    internal static class Program
    {
        private static void Main()
        {
            var server = new WebSocketServer();

            server.OnOpen += (sender, arguments) =>
            {
                WriteLine($"Server is running on port {arguments.Data.Port}.");
            };

            server.OnConnection += (sender, arguments) =>
            {
                WriteLine("Client connected.");
            };

            server.OnMessage += (sender, arguments) =>
            {
                WriteLine($"Received: {arguments.Data.Message}");
                // server.SendData("hi", arguments.Data.Client);
            };

            server.OnClose += (sender, arguments) =>
            {
                WriteLine("Client disconnected.");
            };

            server.Listen();
            ReadLine();
        }
    }
}
