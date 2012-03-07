using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL
{
    class RemoteManager : GameManager
    {
        public RemoteManager()
        {
        }

        public void onGameEvent(GameEvent.GameEvent ge)
        {
            Board board = Board.get();
            switch (ge.type)
            {
                case GameEvent.GameEventType.ChatMessage:
                    GameEvent.ChatMessage cm = (GameEvent.ChatMessage)ge;


                    if (!cm.bounced)
                    {
                        if (LotusGame.get().net != null)
                            LotusGame.get().net.Send(ge);
                    }
                    else
                        LotusGame.get().Chat(cm.message);
                    break;

                case GameEvent.GameEventType.UpdateLobby:
                    GameEvent.UpdateLobby ul = (GameEvent.UpdateLobby)ge;

                    LotusGame.get().SetLobby(ul.lobby);
                    break;
                case GameEvent.GameEventType.SetName:
                    GameEvent.SetName sn = (GameEvent.SetName)ge;
                    
                    if (LotusGame.get().net != null)
                        LotusGame.get().net.Send(ge);
                    break;

                case GameEvent.GameEventType.GameStart:
                    GameEvent.GameStart gs = (GameEvent.GameStart)ge;

                    Player[] players = gs.players;
                    new Board(players);

                    LotusGame.get().LaunchGame(players);
                    break;
                case GameEvent.GameEventType.RegionClick:
                    GameEvent.RegionClick rc = (GameEvent.RegionClick)ge;

                    if (LotusGame.get().net != null)
                        LotusGame.get().net.Send(ge);
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
