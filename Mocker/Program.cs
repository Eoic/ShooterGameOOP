using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mocker
{
    class Program
    {
        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        static void Main(string[] args)
        {
            Connect();
            SendData();
            Console.ReadLine();
        }
        private static void SendData()
        {
            // TODO: Make asynchronous.
            while (true)
            {
                Console.WriteLine("Request: ");
                string req = Console.ReadLine();

                if (req == "c") {
                    _clientSocket.Close();
                    Console.WriteLine("Connection closed.");
                    return;
                }

                byte[] buffer = Encoding.ASCII.GetBytes(req);
                _clientSocket.Send(buffer);

                byte[] resultBuffer = new byte[1024];
                int rec = _clientSocket.Receive(resultBuffer);
                byte[] data = new byte[rec];
                Array.Copy(resultBuffer, data, rec);
                Console.WriteLine("Received: " + Encoding.ASCII.GetString(data));
            }
        }

        private static void Connect()
        {
            int connectionAttempts = 0;

            while (!_clientSocket.Connected)
            {
                try
                {
                    connectionAttempts++;
                    _clientSocket.Connect(IPAddress.Loopback, 8000);
                }
                catch (SocketException)
                {
                    Console.Clear();
                    Console.WriteLine($"Conections attempted: {connectionAttempts}.");
                }

            }

            Console.Clear();
            Console.WriteLine("Connected.");
        }
    }
}
