using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Chat_Server
{
    public class Server
    {
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

                    NetworkStream stream = client.GetStream();
                    Byte[] data = new Byte[256];
                    string message = String.Empty;

                    stream.Read(data, 0, data.Length);
                    message = Encoding.ASCII.GetString(data);

                    Console.WriteLine($"Received: {message}");

                    byte[] returnToClient = Encoding.ASCII.GetBytes(message);

                    // Send back a response.
                    stream.Write(returnToClient, 0, returnToClient.Length);
                    stream.Dispose();
                    client.Close();
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
