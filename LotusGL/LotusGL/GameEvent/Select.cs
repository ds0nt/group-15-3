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

        public override byte[] packMe()
        {
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.MemoryStream());
            writer.Write(pos);
            return ((System.IO.MemoryStream)writer.BaseStream).GetBuffer();
        }
        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            int pos = reader.ReadInt32();
            Console.WriteLine("Pos: " + pos);
            return new Select(pos);
        }
    }
}
