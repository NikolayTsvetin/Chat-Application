using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat_Server
{
    public class Server
    {
        // setup
        private IPAddress address = IPAddress.Parse("127.0.0.1");
        private const int port = 8000;
        private readonly TcpListener server;

        public Server()
        {
            this.server = new TcpListener(address, port);
        }

        public void Start()
        {
            server.Start();

            Console.WriteLine($"Server started on address: {address.ToString()} and port: {port}.");

            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for client connection...");

                    TcpClient client = server.AcceptTcpClient();
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
