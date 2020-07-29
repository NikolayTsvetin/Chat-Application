using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Chat_Server
{
    public class Server
    {
        // setup
        private IPAddress address = IPAddress.Parse("127.0.0.1");
        private const int port = 8000;
        private readonly TcpListener server;
        private static int _clients;

        public Server()
        {
            this.server = new TcpListener(address, port);
        }

        private void HandleClient(TcpClient client)
        {
            Console.WriteLine("Client connected!");

            using (NetworkStream stream = client.GetStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string message = $"[{DateTime.Now}] {sr.ReadToEnd()}";
                    Console.WriteLine($"{message}");

                    SaveMessageToHistory(message);
                    _clients--;
                    client.Close();
                }
            }
        }

        private void SaveMessageToHistory(string message)
        {
            if (!File.Exists(@"./ChatHistory"))
            {
                Directory.CreateDirectory(@"./ChatHistory");
            }

            using (StreamWriter file = new StreamWriter($@"./ChatHistory/{DateTime.Now.ToString("dd/MM/yyyy")}-chat-history.txt", true))
            {
                file.WriteLine($"{message}");
            }
        }

        public void Start()
        {
            server.Start();

            Console.WriteLine($"Server started on address: {address.ToString()} and port: {port}.");

            try
            {
                while (true)
                {
                    if (_clients == 0)
                    {
                        Console.WriteLine("Waiting for client connection...");
                    }

                    TcpClient client = server.AcceptTcpClient();
                    _clients++;
                    Thread connectClient = new Thread(() => HandleClient(client));

                    connectClient.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception thrown: {ex.Message}");
                throw;
            }
            finally
            {
                Console.WriteLine("Shutting server down...");
                server.Stop();
            }
        }
    }
}
