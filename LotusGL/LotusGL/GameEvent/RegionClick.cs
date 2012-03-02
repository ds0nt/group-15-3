using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class RegionClick : GameEvent
    {
        public int pos;
        public Player player;
        public RegionClick(int pos, Player p) : base(GameEventType.RegionClick)
        {
            player = p;
            this.pos = pos;
        }

        public override int[] packMe()
        {
            return new int[]
            {
                pos
            };
        }
    }
}
