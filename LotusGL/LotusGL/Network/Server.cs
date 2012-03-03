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
        public void Start()
        {
            try
            {
                //this is One-to-One connection
                //address = IPAddress.Parse("127.0.0.1"); //This will be removed, 
                listener = new TcpListener(IPAddress.Any, 8010); //and here, instead of address, there will be IPAddress.Any
                listener.Start();
                Console.WriteLine("Server Created. W8 for some clients to connect with me");
                server = listener.AcceptSocket();

                //This will be many-to-many connection
                /*IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 8010);
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                server.Bind(ipep);
                server.Listen(10);

                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddr = host.AddressList[0];
                Console.WriteLine("Computer Name - {0} ", host.HostName);
                Console.WriteLine("IP Addr -" + ipAddr +", Port Num - "+ 8010);
                Console.WriteLine("Server Started.");

                while (false)
                {
                    client = (Socket)server.Accept();
                    Console.WriteLine("Client Connection IP Addr - {0}", client.RemoteEndPoint);
                    //client.BeginReceive(willReceive, 0, 256, SocketFlags.None, new AsyncCallback(CallBack_ReceiveMsg), client);
                }*/
                //new try
                
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
        
        public void Send(string msg)
        {
            try
            {
                willSend = utf8.GetBytes(msg);
                server.Send(willSend);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Receive()
        {
            willReceive = new byte[256];
            server.Receive(willReceive);
            msgToString = utf8.GetString(willReceive);
            msgToString = msgToString.Replace("\0", string.Empty);
            Console.WriteLine("Client: " + msgToString);
        }
    }
}