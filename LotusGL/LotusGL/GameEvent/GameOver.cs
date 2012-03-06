using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class GameOver : GameEvent
    {
        public int windex; // best variable ever
        public GameOver(int winner) : base(GameEventType.GameOver)
        {
            windex = winner;
        }

        public override byte[] packMe()
        {
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.MemoryStream());
            writer.Write(windex);
            return ((System.IO.MemoryStream)writer.BaseStream).GetBuffer();
        }
        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            return new ChangePlayer(reader.ReadInt32());
        }
    }
}
