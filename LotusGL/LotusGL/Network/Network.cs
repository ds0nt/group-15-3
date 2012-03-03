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
    class Network
    {
        public Thread th = null;

        ///////////////////////Variables///////////////////
        //clients
        public Socket client = null;
        public Stream stream = null;
        public TcpClient tcpClient = null;
        //server
        public Socket server = null;
        public IPAddress address = null;
        public TcpListener listener = null;
        //common
        public Encoding utf8 = Encoding.UTF8;
        public byte[] willSend = null;
        public byte[] willReceive = null;
        public string msgToString = null;


        //////////////////functions//////////////// 
        //client
        public bool Connect(string ip)
        {
            return false;
        }
        public void Disconnect() { }

        //server
        public void Start() { }
        public void Stop() { }
        

        //common
        public void Send(string msg) { } //SEEMS SAME BUT CLIENT PART AND SERVER PART WORKS DIFFERENTLY
        public void Receive() { } //SAME HERE. DIFFERENT!
        
        //common2
        public string Get_MyIP()
        {
            IPHostEntry host = Dns.Resolve(Dns.GetHostName());
            string myip = host.AddressList[0].ToString();
            return myip;
        }
    }
}
