using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            // setup - change if needed
            IPAddress address = IPAddress.Parse("127.0.0.1");
            const int port = 8000;

            TcpListener server = new TcpListener(address, port);
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
                    Console.WriteLine("Sent: {0}", message);
                    //stream.Dispose();
                    //client.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ëxception thrown: {ex.Message}");
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
