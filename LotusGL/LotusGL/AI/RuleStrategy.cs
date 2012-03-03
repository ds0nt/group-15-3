using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI
{
    class RuleStrategy : AIStrategy
    {
        public void onBoardChange(Player p, Board b)
        {
        }
        public void doMove(Player p, Board b)
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b);
            Console.WriteLine(moves[0].start + " " + moves[0].end);
            
            LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[0].start, p), 0.1f);
            LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[0].end, p), 0.2f);
        }
    }
}
