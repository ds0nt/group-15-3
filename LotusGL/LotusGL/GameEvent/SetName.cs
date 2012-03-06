using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class SetName : GameEvent
    {
        public string name;
        public SetName(string name)
            : base(GameEventType.SetName)
        {
            this.name = name;
        }

        public override byte[] packMe()
        {
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.MemoryStream());
            writer.Write(name);
            return ((System.IO.MemoryStream)writer.BaseStream).GetBuffer();
        }

        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            return new SetName(reader.ReadString());
        }
    }
}
