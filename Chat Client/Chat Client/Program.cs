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
            string message = String.Empty;

            while (!message.Equals("Exit"))
            {
                using (TcpClient client = new TcpClient("127.0.0.1", 8000))
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            Console.WriteLine("Please, type a message: ");

                            message = Console.ReadLine();
                            sw.Write($"{Environment.UserName}: {message}");

                            Console.WriteLine($"Message sent to server: {message}");
                        }
                    }
                }
            }
        }
    }
}
