using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.State
{
    class AIStateAngry : AIState
    {
        public int emotion = 0;
        public int numberOfTurns = 0;
        StateStrategy stateMachine;
        public List<Move> availableMoves = new List<Move>(); 

        public AIStateAngry(StateStrategy stm)
        {
            Console.WriteLine("now i'm Angry;");
            stateMachine = stm;
        }
        

        public void doMove(Player p, Board b)
        {
            Console.WriteLine("This is the move when i'm angry!!!!!!!!!!!!!!");
            stateMachine.goingTo.coverOpponent(p, b);

            availableMoves = AICalc.getPossibleMoves(p, b);
        }
        public void onBoardChange(Player p, Board b)
        {
            List<Move> newlyAvailableMoves = AICalc.getPossibleMoves(p, b);
            Console.WriteLine(newlyAvailableMoves.Count + " " + availableMoves.Count);
            //if the previous one doens't exist in current then mad!
            for (int i = 0; i < availableMoves.Count; i++)
            {
                Move check = availableMoves[i];
                bool found = false;
                for (int j = 0; j < newlyAvailableMoves.Count; j++)
                {
                    if (check.start == newlyAvailableMoves[j].start)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    emotion++;
                    break;
                }
            }
            


            if (emotion == 2)
            {
                Console.WriteLine("NOW I'm GOING TO MORE MAD!! LET ME BE VENGEFUL!! : " + emotion);
                //return "Angry";
                stateMachine.currentState = new AIStateVengeful(stateMachine);
            }
            else
                Console.WriteLine("not vengeful yet : " + emotion);

            numberOfTurns++;
            if (numberOfTurns == 2)
            {
                Console.WriteLine("now let's go to regular");
                stateMachine.currentState = new AIStateRegular(stateMachine);
            }
            else
                Console.WriteLine("i'm still angry;");
        }
    }
}