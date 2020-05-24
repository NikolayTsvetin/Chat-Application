using System;
using System.Collections.Generic;
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
            string message = String.Empty;

            while (!message.Equals("Exit"))
            {
                TcpClient client = new TcpClient("127.0.0.1", 8000);
                NetworkStream stream = client.GetStream();

                Console.WriteLine("Please, type a message: ");

                message = Console.ReadLine();
                Byte[] data = Encoding.ASCII.GetBytes(message);

                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Message sent to server: {message}");
                stream.Flush();
                data = new Byte[256];
                int serverResponse = stream.Read(data, 0, data.Length);
                string serverResponseMessage = Encoding.ASCII.GetString(data);

                Console.WriteLine($"Received response from server: {serverResponseMessage}");
            }

            //stream.Dispose();
            //stream.Close();
            //client.Close();
        }
    }
}
