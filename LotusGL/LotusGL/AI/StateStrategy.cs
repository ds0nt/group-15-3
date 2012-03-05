using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI
{
    class StateStrategy : AIStrategy
    {
        public int emotion { get; set; }
        public int ableToMovePiece { get; set; }

        public void onBoardChange(Player p, Board b)
        {
            /* vector<int> possibleActiveMoves;

            if (GameData()->board.IsPieceOnTop(this->stateMachine->player->piece, 6) || GameData()->board.IsPieceOnTop(this->stateMachine->player->piece, 7))
            {
                if (GameData()->board.GetSizeOfStack(6) == 4 || GameData()->board.GetSizeOfStack(7) == 3)
                {
                    this->stateMachine->setState(ST_ADVST);
                    return;
                }
                else
                    return;
            }

            if (GameData()->board.IsPieceOnTop(this->stateMachine->player->piece, MAX_GAME_POSITIONS - 1))
            {
                this->stateMachine->setState(ST_RUSH_TO_END);
                return;
            }
            if (GameData()->board.IsPieceOnTop(this->stateMachine->player->piece, MAX_GAME_POSITIONS - 2))
            {
                this->stateMachine->setState(ST_RUSH_TO_END);
                return;
            }
            if (GameData()->board.IsPieceOnTop(this->stateMachine->player->piece, MAX_GAME_POSITIONS - 3))
            {
                this->stateMachine->setState(ST_RUSH_TO_END);
                return;
            }



            vector<move> moves = GameData()->board.getPossibleMoves(this->stateMachine->player->piece);//getting all possible moves.
            vector<move> noDuplicateMoves;

            for (int i = 0; i < moves.size(); i++)
            {
                if (i == 0)
                    noDuplicateMoves.push_back(moves.at(i));
                if (!(i == 0) && !(moves.at(i - 1).beginpos == moves.at(i).beginpos))
                    noDuplicateMoves.push_back(moves.at(i));
            }


            if (noDuplicateMoves.size() < ableToMovePiece)
            {
                cout << "I got attack!?!?!?!>!>!>" << endl;
                ableToMovePiece--;
                emotion++;
            }
            cout << "emotion                     : " << emotion << endl;
            cout << "noDuplicateMoves            : " << noDuplicateMoves.size() << endl;
            cout << "ableToMove in Privious turn : " << ableToMovePiece << endl;



            if (emotion > 2)
                this->stateMachine->setState(ST_ANGRY); */
        }

        public void doMove(Player p, Board b)
        {
            //vector<move> moves = GameData()->board.getPossibleMoves(player.piece); //getting all possible moves.

            //vector<move> okToMove;



            /*for (int i = 0; i < moves.size(); i++)	//so till there is things to move!
            {
                printf("%d, %d |", moves.at(i).beginpos, moves.at(i).endpos); //able to move piece!

                if (!(GameData()->board.IsPieceOnTop(this->stateMachine->player->piece, moves.at(i).endpos)) && (GameData()->board.GetTopPiece(moves.at(i).endpos) != 0))
                {
                    cout << "It's not my piece!! I don't like to attack this guy yet cuz i'm kind!!!" << endl;

                }
                else if (moves.at(i).endpos == 10)
                {
                    int tempDis = GameData()->board.GetSizeOfStack(moves.at(i).beginpos);
                    if (!(GameData()->board.IsPieceOnTop(this->stateMachine->player->piece, moves.at(i).endpos + tempDis)) && (GameData()->board.GetTopPiece(moves.at(i).endpos + tempDis) != 0))
                        cout << "\n\n OMG!! if I use Tramp, enemy will be covered!! i won't do that!!! \n\n" << endl;
                    else
                        okToMove.push_back(moves.at(i));
                }
                else
                {
                    okToMove.push_back(moves.at(i));
                }

            }

            //
            //now make a random move thats NOT COVERING ENEMIES' PIECES!
            if (okToMove.size() != 0)
            {
                int randomMove = rand() % okToMove.size();
                cout << "I move " << okToMove.at(randomMove).beginpos << "," << okToMove.at(randomMove).endpos << endl;
                GameData()->board.MovePiece(okToMove.at(randomMove).beginpos, okToMove.at(randomMove).endpos);
            }
            else if (moves.size() != 0)
            {
                int randomMove = rand() % moves.size();
                cout << "\n\nSO SORRY!!!!!!!!!!!!!!! I had no choice but U!!! Coord is" << moves.at(randomMove).beginpos << "," << moves.at(randomMove).endpos << "\n\n" << endl;
                GameData()->board.MovePiece(moves.at(randomMove).beginpos, moves.at(randomMove).endpos);
            }
            else // super NO CHOICE!!! I don't have any piece left to 
            {
                cout << "\n\nI'll jsut SKIP the turn cuz i'm kind!!!\n\n" << endl;
            }





            //here I need to keep a track of my availabe pieces.
            moves = GameData()->board.getPossibleMoves(player.piece);
            vector<move> noDuplicateMoves;

            for (int i = 0; i < moves.size(); i++)
            {
                if (i == 0)
                    noDuplicateMoves.push_back(moves.at(i));
                if (!(i == 0) && !(moves.at(i - 1).beginpos == moves.at(i).beginpos))
                    noDuplicateMoves.push_back(moves.at(i));
            }

            ableToMovePiece = noDuplicateMoves.size();
            return;
        }*/
        }
    }
}