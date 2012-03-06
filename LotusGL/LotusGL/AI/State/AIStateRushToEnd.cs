using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.State
{
    class AIStateRushToEnd : AIState
    {
        public AIStateRushToEnd(StateStrategy stateMachine)
        {
            // this->stateMachine = stateMachine;
        }

        public void onBoardChange(Player p, Board b)
        {
            /* for (int i = MAX_GAME_POSITIONS - 1; i >= MAX_GAME_POSITIONS - 3; i--)
            {
                if (GameData()->board.IsPieceOnTop(this->stateMachine->player->piece, i))
                {
                    return;
                }
            }
            this->stateMachine->setState(ST_REGULAR); */
        }

        public void doMove(Player p, Board b)
        {
            /* for (int i = MAX_GAME_POSITIONS - 1; i >= MAX_GAME_POSITIONS - 3; i--)
            {
                if (GameData()->board.IsPieceOnTop(this->stateMachine->player->piece, i))
                {
                    GameData()->board.MovePiece(i, -1);
                    return;
                }
            } */
        }
    }
}