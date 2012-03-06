using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI
{
    class AIMoves //This class has all of the moves that AI can take. THIS WILL B VERY CONVINIENT!
    {


        /////////////////////////////General Moves////////////////////////////////

        //random Move
        public void moveRandom(Player p, Board b) // do random
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b); // using AI calc to get the possible moves.
            if (moves.Count == 0)
            {
                moveRandomOpponentsPiece(p, b);
            }
            else
            {
                int moveid = AICalc.rand.Next(0, moves.Count - 1); // rand means it randers!
                Console.WriteLine("AIMoves . Function Random move called!!!");
                Console.WriteLine("Move I make: " + moves[moveid].start + " to " + moves[moveid].end);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].start, LotusGame.get().currentPlayer), 0.1f);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].end, LotusGame.get().currentPlayer), 0.2f);
            }
        }
        //move the highest one!
        public void moveHighest(Player p, Board b)
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b); // using AI calc to get the possible moves.
            if (moves.Count == 0)
            {
                moveRandomOpponentsPiece(p, b);
            }
            else
            {
                int moveid = AICalc.rand.Next(0, moves.Count - 1); // rand means it randers!
                Console.WriteLine("AIMoves . Function highest move called!!!");
                Console.WriteLine("Move I make: " + moves[moveid].start + " to " + moves[moveid].end);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].start, LotusGame.get().currentPlayer), 0.1f);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].end, LotusGame.get().currentPlayer), 0.2f);
            }
        }
        //move the one that's close to the goal.
        public void moveClosestToGoal(Player p, Board b)
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b); // using AI calc to get the possible moves.
            if (moves.Count == 0)
            {
                moveRandomOpponentsPiece(p, b);
            }
            else
            {
                int moveid = AICalc.rand.Next(0, moves.Count - 1); // rand means it randers!
                Console.WriteLine("AIMoves . Function ClosestToGoal move called!!!");
                Console.WriteLine("Move I make: " + moves[moveid].start + " to " + moves[moveid].end);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].start, LotusGame.get().currentPlayer), 0.1f);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].end, LotusGame.get().currentPlayer), 0.2f);
            }
        }
        //move the one that's close to the start position.
        public void moveStartPosition(Player p, Board b)
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b); // using AI calc to get the possible moves.
            if (moves.Count == 0)
            {
                moveRandomOpponentsPiece(p, b);
            }
            else
            {
                int moveid = AICalc.rand.Next(0, moves.Count - 1); // rand means it randers!
                Console.WriteLine("AIMoves . Function StartPosition move called!!!");
                Console.WriteLine("Move I make: " + moves[moveid].start + " to " + moves[moveid].end);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].start, LotusGame.get().currentPlayer), 0.1f);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].end, LotusGame.get().currentPlayer), 0.2f);
            }
        }
        /* New rule should follow following format:
        public void moveNew(Player p, Board b)
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b); // using AI calc to get the possible moves.
            if (moves.Count == 0)
            {
                moveRandomOpponentsPiece(p, b);
            }
            else
            {
                int moveid;
                ----------Coding
                int moveid = AICalc.rand.Next(0, moves.Count - 1); // rand means it randers!
                ----------coding
                Console.WriteLine("AIMoves . Function New move called!!!");
                Console.WriteLine("Move I make: " + moves[moveid].start + " to " + moves[moveid].end);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].start, LotusGame.get().currentPlayer), 0.1f);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].end, LotusGame.get().currentPlayer), 0.2f);
            }
        }
        */




        ///////////////////////Has no Move Handler////////////////////////////
        public void moveRandomOpponentsPiece(Player p, Board b)
        {
            Console.WriteLine(p.name + " Has No Moves!"); // if this guy has no move
            while (true)
            {
                int i = AICalc.rand.Next(0, LotusGame.get().players.Length);
      
                if (LotusGame.get().players[i] != p)
                {
                    List<Move> moves = AICalc.getPossibleMoves(LotusGame.get().players[i], b);
                    if (moves.Count != 0)
                    {
                        int moveid = AICalc.rand.Next(0, moves.Count - 1);
                        Console.WriteLine("Move I make: " + moves[moveid].start + " to " + moves[moveid].end);
                        LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].start, i), 0.1f);
                        LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].end, i), 0.2f);
                        break;
                    }
                }
            }
        }
    }
}
