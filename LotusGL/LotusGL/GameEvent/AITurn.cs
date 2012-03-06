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

        public override byte[] packMe()
        {
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.MemoryStream());
            writer.Write(ai);

            return ((System.IO.MemoryStream)writer.BaseStream).GetBuffer();
        }
    }
}
