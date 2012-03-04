using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class Select : GameEvent
    {
        public int pos;
        public Select(int pos) : base(GameEventType.SelectPiece)
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
        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            int pos = reader.ReadInt32();
            Console.WriteLine("Pos: " + pos);
            return new Select(pos);
        }
    }
}
