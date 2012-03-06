using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.State
{
    class AIState
    {
        virtual public void onBoardChange(Player p, Board b){}
	    virtual public void doTurn(Player p, Board b){}
    }
    class StateStrategy : AIStrategy
    {
        AIState currentState;

        public StateStrategy()
        {
            currentState = new AIStateRegular(this);
        }
        public void setState(string newState)
        {
            switch (newState)
            {
                case "Regular":
                    currentState = new AIStateRegular(this);
                    break;
                case "Angry":
                    currentState = new AIStateRegular(this);
                    break;
                case "Vengeful":
                    currentState = new AIStateRegular(this);
                    break;
                case "RushToEnd":
                    currentState = new AIStateRegular(this);
                    break;
                case "AIStateAdvST":
                    currentState = new AIStateRegular(this);
                    break;
            }
            
        }
        
        public void doMove(Player p, Board b)
        {      
        }


        public void onBoardChange(Player p, Board b)
        {
        }
    }
}