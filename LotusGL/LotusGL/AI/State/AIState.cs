using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.State
{
    interface AIState
    {
        void doMove(Player p, Board b);
        void onBoardChange(Player p, Board b);
    }
}
