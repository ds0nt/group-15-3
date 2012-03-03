using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI
{
    interface AIStrategy
    {
        void onBoardChange(Player p, Board b);
        void doMove(Player p, Board b);
    }
}
