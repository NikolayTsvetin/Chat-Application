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
        private int _clients;

        public Server()
        {
            this.server = new TcpListener(address, port);
            this._clients = 0;
        }

        private void HandleClient(TcpClient client)
        {
            Console.WriteLine("Client connected!");

            using (NetworkStream stream = client.GetStream())
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    string message = sr.ReadToEnd();
                    Console.WriteLine($"{message}");

                    client.Close();
                    // TODO: Show whoever sends a message the whole conversation (in the case that it is public chatroom and everybody can read all the messages)
                }
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
                    if (this._clients == 0)
                    {
                        Console.WriteLine("Waiting for client connection...");
                    }

                    TcpClient client = server.AcceptTcpClient();
                    this._clients++;
                    Thread connectClient = new Thread(() => HandleClient(client));

                    connectClient.Start();
                    this._clients--;
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
