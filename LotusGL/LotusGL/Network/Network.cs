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
        public Socket server = null;

        public TcpClient tcp;

        public IPAddress address = null;
        
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

            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        public GameEvent.GameEvent Receive()
        {
            byte[] willReceive = new byte[4];

            
            BinaryReader reader = new BinaryReader(stream);
            if (tcp.Available < 8)
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
