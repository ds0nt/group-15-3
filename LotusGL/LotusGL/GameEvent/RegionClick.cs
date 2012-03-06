using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class RegionClick : GameEvent
    {
        public int pos;
        public int player;
        public RegionClick(int pos, int p) : base(GameEventType.RegionClick)
        {
            player = p;
            this.pos = pos;
        }

        public override byte[] packMe()
        {
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.MemoryStream());
            writer.Write(pos);
            writer.Write(player);
            return ((System.IO.MemoryStream)writer.BaseStream).GetBuffer();
        }

        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            return new RegionClick(reader.ReadInt32(), reader.ReadInt32());
        }
    }
}
