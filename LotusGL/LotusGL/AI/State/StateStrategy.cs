using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.State
{
    class StateStrategy : AIStrategy
    {
        public AIState currentState;
        public AIMoves goingTo = new AIMoves();
        //public string stateName;

        //public AIState regular; //= new AIStateRegular();
        //public AIState angry; //= new AIStateAngry();
        //public AIState vengeful; //= new AIStateVengeful();
        //public AIState rushToEnd;// = new AIStateRushToEnd();
        //public AIState advST;// = new AIStateAdvST();
        /*
        //public string state;
        
        public void changeState(string newState)
        {
            switch (newState)
            {
                case "Regular":
                    regular = new AIStateRegular(this);
                    currentState = regular;
                    break;
                case "Angry":
                    angry = new AIStateAngry(this);
                    currentState = angry;
                    break;
                case "Vengeful":
                    //vengeful = new AIStateVengeful(this);
                    currentState = vengeful;
                    break;
                case "RushToEnd":
                    //rushToEnd = new AIStateRegular(this);
                    currentState = rushToEnd;
                    break;
                case "AIStateAdvST":
                    advST = new AIStateRegular(this);
                    currentState = advST;
                    break;
            }
        }
        */
        public StateStrategy()
        {
            currentState = new AIStateRegular(this);
            //state = "Regular";
        }
        public void doMove(Player p, Board b)
        {
            currentState.doMove(p, b);
        }


        public void onBoardChange(Player p, Board b)
        {
            currentState.onBoardChange(p, b);
        }
    }
}