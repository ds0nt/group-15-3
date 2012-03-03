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
    }
}
