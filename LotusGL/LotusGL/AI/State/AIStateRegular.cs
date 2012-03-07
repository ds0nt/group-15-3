using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.State
{
    class AIStateRegular : AIState
    {

        int emotion=0;
        StateStrategy stateMachine;
        public AIStateRegular(StateStrategy stm)
        {

            stateMachine = stm;
        }
        public void doMove(Player p, Board b)
        {
            //emotion++;
            Console.WriteLine("regular doMove" + emotion);
            stateMachine.goingTo.moveRandom(p, b);
            
        }
        public void onBoardChange(Player p, Board b)
        {
            //string toState=null;
            emotion++;
            Console.WriteLine("the omtion change: " + emotion);
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
