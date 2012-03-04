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

        public override int[] packMe()
        {
            return new int[]
            {
                windex
            };
        }
        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            return new ChangePlayer(reader.ReadInt32());
        }
    }
}
