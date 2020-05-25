using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = null;
            NetworkStream stream = null;
            string message = String.Empty;

            while (!message.Equals("Exit"))
            {
                client = new TcpClient("127.0.0.1", 8000);
                stream = client.GetStream();
                StreamWriter sw = new StreamWriter(stream);

                Console.WriteLine("Please, type a message: ");

                message = Console.ReadLine();
                sw.Write($"{Environment.UserName}: {message}");

                Console.WriteLine($"Message sent to server: {message}");

                sw.Dispose();
            }

            stream?.Dispose();
            stream?.Close();
            client?.Close();
        }
    }
}
