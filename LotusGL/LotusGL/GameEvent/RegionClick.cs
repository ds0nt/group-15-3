using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class RegionClick : GameEvent
    {
        public int pos;
        public string name;
        public RegionClick(int pos)
            : base(GameEventType.RegionClick)
        {
            this.pos = pos;
            this.name = LotusGame.get().name;
        }
        public RegionClick(int pos, string name) : base(GameEventType.RegionClick)
        {
            this.name = name;
            this.pos = pos;
        }

        public override byte[] packMe()
        {
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.MemoryStream());
            writer.Write(pos);
            writer.Write(name);
            return ((System.IO.MemoryStream)writer.BaseStream).GetBuffer();
        }

        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            return new RegionClick(reader.ReadInt32(), reader.ReadString());
        }
    }
}
