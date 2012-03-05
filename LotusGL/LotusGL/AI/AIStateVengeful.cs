using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI
{
    class AIStateVengeful : AIStrategy
    {
        public int enactedCount { get; set; }

        public AIStateVengeful()
        {
            /* this->stateMachine = stateMachine;
            //int target;
            if ((GameData()->players.at(GameData()->currentPlayer).piece - 1) == 0)
            {
                gettinSum = GameData()->players.at(GameData()->numplayers - 1).piece;
            }
            else
                gettinSum = GameData()->players.at(GameData()->currentPlayer - 1).piece;
            //this->gettinSum = GameData()->players.at(GameData()->currentPlayer - 1).piece;
            this->enactedCount = 0;
            cout << "\nI'm Vengeful!! i'll target a specific player and only attack that guy!!\n" << endl;
            cout << "\nI attacked " << this->gettinSum << " guy " << this->enactedCount << " times\n" << endl; */
        }

        public void onBoardChange(Player p, Board b)
        {
            /* if (this->enactedCount > 1)
                this->stateMachine->setState(ST_REGULAR); */
        }

        public void doMove(Player p, Board b)
        {
            /* vector<move> moves = GameData()->board.getPossibleMoves(player.piece);
            cout << "got here" << this->gettinSum << endl;
            cout << "And I am" << this->stateMachine->player->piece << endl;
            for (int i = 0; i < moves.size(); i++)
            {
                if (GameData()->board.IsPieceOnTop(this->gettinSum, moves.at(i).endpos))
                {
                    cout << "got here tOOOOOOOOOOOOOOOOOOOOOOOOOO" << endl;
                    GameData()->board.MovePiece(moves.at(i).beginpos, moves.at(i).endpos);
                    this->enactedCount++;
                    cout << "\nI attacked " << this->gettinSum << " guy " << this->enactedCount << " times\n" << endl;
                    return;
                }
            }
            //if theres none of his piece we can land on, just move something.
            for (int i = 0; i < moves.size(); i++)
            {
                GameData()->board.MovePiece(moves.at(i).beginpos, moves.at(i).endpos);
                return;
            }
            //Otherwise skip turn */
        }
    }
}