using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.State
{
    class AIStateAdvST : AIStrategy
    {
        public AIStateAdvST()
        {
            /* this->stateMachine = stateMachine;
            printf("********************IN Advance Springtile!******************\n"); */
        }
        public void onBoardChange(Player p, Board b)
        {
            /* printf("@@@@@@@@@@AI had(or used) a chance of going to Springtile...@@@@@@@@@@\n");
            this->stateMachine->setState(ST_REGULAR); */
        }

        public void doMove(Player p, Board b)
        {
            /* printf("#############Doing AdvSpringTile move!!!#############\n");
            Sleep(1000);
            vector<move> moves = GameData()->board.getPossibleMoves(player.piece);
            
            for (int i = 0; i < moves.size(); i++)
            {
                if (moves.at(i).endpos == 10 && (GameData()->board.GetSizeOfStack(moves.at(i).beginpos) == 4 || GameData()->board.GetSizeOfStack(moves.at(i).beginpos) == 3))
                {
                    GameData()->board.MovePiece(moves.at(i).beginpos, moves.at(i).endpos);
                    return;
                }
            } */
        }
    }
}