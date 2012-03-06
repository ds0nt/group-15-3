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
        public List<Socket> connections;
        public List<NetworkStream> streams;

        bool listening;

        public void StartListen()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 8010);
                listener.Start();

                Console.WriteLine("Server Created. W8 for some clients to connect with me");

                connections = new List<Socket>();
                streams = new List<NetworkStream>();
                listening = true;
                listener.BeginAcceptSocket(accepter, listener);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void EndListen()
        {
            listening = false;
        }

        public void accepter(IAsyncResult res)
        { 
            TcpListener listener = (TcpListener) res.AsyncState;

            Socket clientSocket = listener.EndAcceptSocket(res);
            lock (this)
            {
                if (listening)
                {
                    connections.Add(clientSocket);
                    streams.Add(new NetworkStream(clientSocket));
                    Console.WriteLine("Client connected completed");
                    listener.BeginAcceptSocket(accepter, listener);
                }
                else
                {
                    clientSocket.Close();
                    Console.WriteLine("Rejecting Client");
                }
            }
        }

        //Server Stop
        public void Stop()
        {
            try
            {
                listener.Stop();
                Console.WriteLine("Server Closed. All the resource is back and all clients are disconnected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Send(GameEvent.GameEvent willSend)
        {
            int[] data = willSend.packMe();

            byte[] bytes = new byte[(data.Length + 2) * sizeof(int)];

            BinaryWriter writer = new BinaryWriter(new MemoryStream(bytes));
            writer.Write((data.Length + 1) * sizeof(int));
            writer.Write((int)willSend.type);
            for (int i = 0; i < data.Length; i++)
                writer.Write(data[i]);
            writer.Close();

            foreach (NetworkStream stream in streams)
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Flush();
            }
        }

        public GameEvent.GameEvent Receive()
        {
            byte[] willReceive = new byte[4];


            foreach (NetworkStream stream in streams)
            {
                BinaryReader reader = new BinaryReader(stream);
                if (!stream.DataAvailable)
                    continue;

                int datalen = reader.ReadInt32();
                Console.WriteLine(datalen);
                if (datalen > 0)
                {
                    willReceive = new byte[datalen];
                    int readlen = stream.Read(willReceive, 0, datalen);

                    if (datalen == readlen)
                    {
                        return GameEvent.GameEvent.parseBytes(willReceive);
                    }
                }
            }
            return null;
        }
    }
}