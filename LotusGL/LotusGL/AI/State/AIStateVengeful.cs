using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.State
{
    class AIStateVengeful : AIState
    {
        public int numberOfTurns = 0;
        public StateStrategy stateMachine;
        //public int enactedCount { get; set; }

        public AIStateVengeful(StateStrategy stm)
        {
            stateMachine = stm;
            Console.WriteLine("NOW I'm Vengeful!!!!!!!!!!! I only target one guy and only kill that guy!!");
        }

        public void onBoardChange(Player p, Board b)
        {
            Console.WriteLine("This is my REVENGE MOVE!!!!!!! HAHAHAHHAHAHA!!!!");
            stateMachine.goingTo.coverOpponent(p, b);
        }

        public void doMove(Player p, Board b)
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