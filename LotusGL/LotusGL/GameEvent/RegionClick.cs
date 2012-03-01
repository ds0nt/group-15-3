using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class RegionClick : GameEvent
    {
        public int pos;
        public RegionClick(int pos) : base(GameEventType.RegionClick)
        {
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
