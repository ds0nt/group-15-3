using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.State
{
    class AIStateRegular : AIState
    {
        StateStrategy state;
        int emotion { get; set; }
        public AIStateRegular(StateStrategy stateMachine)
        {
            this.state = stateMachine;
        }
        public void doTurn(Player p, Board b)
        {
            LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(-100, LotusGame.get().currentPlayer), 0.1f);
        }
        public void onBoardChanbe(Player p, Board b)
        {
        }
    }
}
