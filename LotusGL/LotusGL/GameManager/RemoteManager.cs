using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL
{
    class RemoteManager : GameManager
    {
        Board board;

        public RemoteManager(Board b)
        {
            board = b;
        }

        public void onGameEvent(GameEvent.GameEvent ge)
        {
            switch (ge.type)
            {
                case GameEvent.GameEventType.RegionClick:
                    GameEvent.RegionClick rc = (GameEvent.RegionClick)ge;

                    break;

                case GameEvent.GameEventType.MovePiece:
                    GameEvent.Move move = (GameEvent.Move)ge;
                    board.movePiece(move.frompos, move.topos);
                    board.selectedId = int.MinValue;
                    break;


                case GameEvent.GameEventType.SelectPiece:
                    GameEvent.Select select = (GameEvent.Select)ge;

                    board.selectedId = select.pos;
                    break;


                case GameEvent.GameEventType.GameOver:
                    GameEvent.GameOver gameover = (GameEvent.GameOver)ge;


                    break;
                case GameEvent.GameEventType.AITurn:
                    break;
                case GameEvent.GameEventType.SetPlayer:
                    GameEvent.ChangePlayer playerchange = (GameEvent.ChangePlayer)ge;
                    LotusGame.get().setCurrentPlayer(playerchange.player);
                    break;
            }
        }
    }
}
