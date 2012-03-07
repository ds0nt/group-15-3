using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.State
{
    class AIStateRegular : AIState
    {

        public int emotion=0;
        public StateStrategy stateMachine;
        public List<Move> availableMoves = new List<Move>(); 
        

        public AIStateRegular(StateStrategy stm)
        {

            stateMachine = stm;
        }
        public void doMove(Player p, Board b)
        {
            Console.WriteLine("regular doMove" + emotion);
            stateMachine.goingTo.moveRandom(p, b);
            
            availableMoves = AICalc.getPossibleMoves(p, b);
            

        }
        public void onBoardChange(Player p, Board b)
        {
            List<Move> newlyAvailableMoves = AICalc.getPossibleMoves(p,b);
            Console.WriteLine(newlyAvailableMoves.Count + " " + availableMoves.Count);
            //if the previous one doens't exist in current then mad!
            for (int i = 0; i < availableMoves.Count; i++)
            {
                bool found = false;
                for (int j = 0; j < newlyAvailableMoves.Count; j++)
                {
                    if (availableMoves[i].start == newlyAvailableMoves[j].start)
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
                Console.WriteLine("i;m angry cuz : " + emotion);
                stateMachine.currentState = new AIStateAngry(stateMachine);
            }
            else
                Console.WriteLine("not yet : " + emotion);
        
        }
    }
}
