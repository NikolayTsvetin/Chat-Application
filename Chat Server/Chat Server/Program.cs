﻿using System;
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
            Server server = new Server();
            server.Start();
        }
    }
}