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
        NetworkStream stream;
        Socket sock;

        public bool Connect(string ip)
        {
            try
            {

                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Connect using a timeout (5 seconds)

                IAsyncResult result = sock.BeginConnect(ip, 8010, null, null);



                Console.WriteLine("Connecting");

                bool success = result.AsyncWaitHandle.WaitOne(2000, true);
                if (sock.Connected)
                {
                    stream = new NetworkStream(sock);

                    LotusGame.get().Chat("Connection Established");
                    Console.WriteLine("Connection Established");
                }
                else
                {
                    LotusGame.get().Chat("Connection Failed");
                    Console.WriteLine("Connection Failed");
                }
                
                return sock.Connected;
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
                sock.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Send(GameEvent.GameEvent willSend)
        {
            byte[] data = willSend.packMe();

            byte[] bytes = new byte[(data.Length + 2 * sizeof(int))];

            BinaryWriter writer = new BinaryWriter(new MemoryStream(bytes));
            writer.Write((data.Length + 1 * sizeof(int)));
            writer.Write((int)willSend.type);
            for (int i = 0; i < data.Length; i++)
                writer.Write(data[i]);
            writer.Close();

            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        public GameEvent.GameEvent Receive()
        {
            byte[] willReceive = new byte[4];

            BinaryReader reader = new BinaryReader(stream);
            
            if (!stream.DataAvailable)
                return null;

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
            return null;
        }

    }
}
