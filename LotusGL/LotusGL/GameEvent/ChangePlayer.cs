using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class ChangePlayer : GameEvent
    {
        public int player;
        
        public ChangePlayer(int player)
            : base(GameEventType.SetPlayer)
        {
            this.player = player;
        }

        public override int[] packMe()
        {
            return new int[]
            {
                player
            };
        }

        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            return new ChangePlayer(reader.ReadInt32());
        }
    }
}
