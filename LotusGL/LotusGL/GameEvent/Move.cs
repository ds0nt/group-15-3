using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class Move : GameEvent
    {
        public int frompos, topos;
        public Move(int frompos, int topos) : base(GameEventType.MovePiece)
        {
            this.frompos = frompos;
            this.topos = topos;
        }

        public override byte[] packMe()
        {
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.MemoryStream());
            writer.Write(frompos);
            writer.Write(topos);
            return ((System.IO.MemoryStream)writer.BaseStream).GetBuffer();
        }
        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            return new Move(reader.ReadInt32(), reader.ReadInt32());
        }
    }
}

