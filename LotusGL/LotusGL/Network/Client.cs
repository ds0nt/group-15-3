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
    class Client : Network
    {
        public bool Connect(string ip)
        {
            try
            {
                tcp = new TcpClient();
                tcp.Connect(ip, 8010);
                stream = tcp.GetStream();
                Console.WriteLine("Connected to Server Successfully!!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public void Disconnect()
        {
            try
            {
                if (client != null)
                {
                    if (client.Connected)
                        client.Close();
                    if (th.IsAlive)
                        th.Abort();
                }
                Console.WriteLine("Client is disconnected");
                //client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
