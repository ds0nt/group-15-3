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
        public AIStateAngry(StateStrategy stm)
        {
            Console.WriteLine("now i'm Angry;");
            stateMachine = stm;
        }
        

        public void doMove(Player p, Board b)
        {
            Console.WriteLine("This is the move when i'm angry");
            
        }
        public void onBoardChange(Player p, Board b)
        {
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