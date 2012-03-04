using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace LotusGL.Network
{
    class Server : Network
    {

        public TcpListener listener = null;

        public void Start()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 8010);
                listener.Start();
                Console.WriteLine("Server Created. W8 for some clients to connect with me");

                tcp = listener.AcceptTcpClient();
                stream = tcp.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        //Server Stop
        public void Stop()
        {
            try
            {
                listener.Stop();
                server.Close();
                Console.WriteLine("Server Closed. All the resource is back and all clients are disconnected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
    }
}