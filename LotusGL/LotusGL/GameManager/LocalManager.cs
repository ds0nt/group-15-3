using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL
{
    class LocalManager : GameManager
    {
        Board board;
        LotusGame game;
        public LocalManager(Board b, LotusGame g)
        {
            board = b;
            game = g;
        }

        public void onGameEvent(GameEvent.GameEvent ge)
        {
            switch (ge.type)
            {
                case GameEvent.GameEventType.RegionClick:
                    GameEvent.RegionClick rc = (GameEvent.RegionClick)ge;

                    if (board.selectedId == int.MinValue)
                        game.FireEvent(new GameEvent.Select(rc.pos));
                    else if (board.selectedId == rc.pos)
                        game.FireEvent(new GameEvent.Select(int.MinValue));
                    else
                        game.FireEvent(new GameEvent.Move(board.selectedId, rc.pos));
                    break;


                case GameEvent.GameEventType.MovePiece:
                    GameEvent.Move move = (GameEvent.Move)ge;

                    board.selectedId = int.MinValue;
                    if(move.frompos != move.topos)
                        board.movePiece(move.frompos, move.topos);
                    break;


                case GameEvent.GameEventType.SelectPiece:
                    GameEvent.Select select = (GameEvent.Select)ge;

                    board.selectedId = select.pos;
                    break;
            }
        }
    }
}
