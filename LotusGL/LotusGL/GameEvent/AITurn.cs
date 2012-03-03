using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class AITurn : GameEvent
    {
        public int ai; // best variable ever
        public AITurn(int ai)
            : base(GameEventType.AITurn)
        {
            this.ai = ai;
        }

        public override int[] packMe()
        {
            return new int[]
            {
                ai
            };
        }
    }
}
