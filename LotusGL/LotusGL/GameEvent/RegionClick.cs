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

        public override int[] packMe()
        {
            return new int[]
            {
                pos,
                player
            };
        }
        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            return new RegionClick(reader.ReadInt32(), reader.ReadInt32());
        }
    }
}
