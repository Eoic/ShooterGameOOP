using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Mocker
{
    class Program
    {
        private static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private const int MaxAttempts = 5;
        
        static void Main(string[] args)
        {
            if (Connect()) {
                SendData();
            }
            
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

        private static bool Connect()
        {
            var connectionAttempts = 0;

            while (!_clientSocket.Connected)
            {
                try
                {
                    if (connectionAttempts < MaxAttempts)
                    {
                        connectionAttempts++;
                        _clientSocket.Connect(IPAddress.Loopback, 8000);
                    }
                    else
                    {
                        Console.WriteLine("Could not connect to server. Exiting.");
                        return false;
                    }
                }
                catch (SocketException)
                {
                    Console.WriteLine($"Connections attempted: {connectionAttempts}.");
                    return false;
                }

            }

            Console.Clear();
            Console.WriteLine("Connected.");
            return true;
        }
    }
}
