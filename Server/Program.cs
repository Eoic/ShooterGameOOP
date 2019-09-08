using System.Collections.Generic;
using System.Dynamic;
using Server.Core;
using Server.Utilities;
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

            var parser = new JsonParser();
            dynamic obj = new ExpandoObject();
            obj.message = "Hello";
            obj.number = 42;
            obj.position = new ExpandoObject();
            obj.position.x = 0;
            
            dynamic a = new ExpandoObject();
            a.y = 54;

            obj.positionN = a;

            WriteLine("Serializing.");
            var result = parser.Serialize(obj);
            WriteLine(result);
            
            WriteLine("Deserializing");
            var resultObj = parser.Deserialize(result);
            WriteLine(resultObj.message);
            WriteLine(resultObj.number);
            WriteLine(resultObj.position[0]);
            var s = resultObj.positionN[0];

            //server.Listen();
            ReadLine();
        }
    }
}
