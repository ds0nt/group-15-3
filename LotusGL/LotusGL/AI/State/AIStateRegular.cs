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
        public int numberOfAvailables;
        public AIStateRegular(StateStrategy stm)
        {

            stateMachine = stm;
        }
        public void doMove(Player p, Board b)
        {
            //emotion++;

            Console.WriteLine("regular doMove" + emotion);
            stateMachine.goingTo.moveRandom(p, b);
            
            availableMoves = AICalc.getPossibleMoves(p, b);
            //numberOfAvailables = AICalc.getPossibleMoves(p, b).Count;

        }
        public void onBoardChange(Player p, Board b)
        {
            List<Move> newlyAvailableMoves = AICalc.getPossibleMoves(p,b);
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
            //if (newlyAvailableMoves.Count < availableMoves.Count)
            //{
                //emotion++;
            //if(emotion == 2)
            //    Console.WriteLine("the emotion change: " + emotion);
            //}
            //else
            //    Console.WriteLine("i didn't get emotion ++");


            
            if (emotion == 2)
            {
                Console.WriteLine("i;m angry cuz : " + emotion);
                //return "Angry";
                stateMachine.currentState = new AIStateAngry(stateMachine);
            }
            else
                Console.WriteLine("not yet : " + emotion);
                //return "Regular";

            //return toState;
        }


        /*
        StateStrategy state;
        int emotion{ get; set; }
        public AIStateRegular()
        {
            Console.WriteLine("This too??");
            //this.state = stateMachine;
            emotion = 0;
            emotion++;
            Console.WriteLine("This is do Turn. Emotion:" + emotion);
        }
        public void doTurn(Player p, Board b)
        {
            Console.WriteLine("This is do Turn. Emotion:" + emotion);
            LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(-100), 0.1f);
        }
        public void onBoardChanbe(Player p, Board b)
        {
            System.Timers.Timer aTimer = new System.Timers.Timer();///////////////////
            emotion++;
            Console.WriteLine("This is onBoardChange. Emotion:" + emotion);
            //aTimer.Interval=5000;
            //aTimer.Enabled = true;
        }*/
    }
}
