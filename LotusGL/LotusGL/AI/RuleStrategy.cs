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
            if (moves.Count == 0)
            {
                //Console.WriteLine(p.name + " Has No Moves!");
                for (int i = 0; i < LotusGame.get().players.Length; i++)
                {
                    if (LotusGame.get().players[i] != p)
                    {
                        moves = AICalc.getPossibleMoves(LotusGame.get().players[i], b);
                        if (moves.Count != 0)
                        {
                            //Console.WriteLine("Moving " + LotusGame.get().players[i].name + "'s Piece!");
                            break;
                        }
                    }
                }
            }
            int moveid = AICalc.rand.Next(0, moves.Count - 1);

            Console.WriteLine(moves[moveid].start + " " + moves[moveid].end);
            LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].start, LotusGame.get().currentPlayer), 0.1f);
            LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].end, LotusGame.get().currentPlayer), 0.2f);
        }
    }
}
