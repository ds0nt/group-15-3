using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class ChatMessage : GameEvent
    {
        public string message;
        public bool bounced;

        public ChatMessage(string message, bool bounced = false)
            : base(GameEventType.ChatMessage)
        {
            this.bounced = bounced;
            this.message = message;
        }

        public override byte[] packMe()
        {
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.MemoryStream());
            writer.Write(message);
            writer.Write(bounced);
            return ((System.IO.MemoryStream)writer.BaseStream).GetBuffer();
        }

        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            return new ChatMessage(reader.ReadString(), reader.ReadBoolean());
        }
    }
}
