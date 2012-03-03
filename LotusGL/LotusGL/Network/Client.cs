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
    class Client: Network
    {
        public bool Connect(string ip)
        {
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(ip, 8010);
                stream = tcpClient.GetStream();
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
        public void Send(string msg)
        {
            try
            {
                willSend = utf8.GetBytes(msg);
                stream.Write(willSend, 0, willSend.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Receive()
        {
            try
            {
                willReceive = new byte[256];
                stream.Read(willReceive, 0, 256);
                msgToString = utf8.GetString(willReceive);
                msgToString = msgToString.Replace("\0", string.Empty);
                Console.WriteLine("Server: " + msgToString);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
