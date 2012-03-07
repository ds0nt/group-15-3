using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL
{
    class LocalManager : GameManager
    {
        int currentPlayer;
        public int getCurrentPlayerID()
        {
            return currentPlayer;
        }

        public LocalManager()
        {
        }

        public void onGameEvent(GameEvent.GameEvent ge)
        {
            Board board = Board.get();
            switch (ge.type)
            {
                case GameEvent.GameEventType.ChatMessage:
                    GameEvent.ChatMessage cm = (GameEvent.ChatMessage)ge;

                    LotusGame.get().Chat(cm.message);

                    if (LotusGame.get().net != null)
                    {
                        cm.bounced = true;
                        LotusGame.get().net.Send(ge);
                    }
                    break;
                case GameEvent.GameEventType.UpdateLobby:
                    GameEvent.UpdateLobby ul = (GameEvent.UpdateLobby)ge;

                    if (LotusGame.get().net != null)
                        LotusGame.get().net.Send(ge);
                    break;

                case GameEvent.GameEventType.SetName:
                    GameEvent.SetName sn = (GameEvent.SetName)ge;
                    LotusGame.get().AddName(sn.name);
                    break;
                case GameEvent.GameEventType.GameStart:
                    GameEvent.GameStart gs = (GameEvent.GameStart)ge;

                    new Board(gs.players);

                    if (LotusGame.get().net != null)
                        LotusGame.get().net.Send(ge);

                    LotusGame.get().LaunchGame(gs.players);
                    currentPlayer = -1;
                    cyclePlayer();
                    break;
                case GameEvent.GameEventType.RegionClick:
                    GameEvent.RegionClick rc = (GameEvent.RegionClick)ge;
                    if (rc.name == LotusGame.get().players[currentPlayer].name || LotusGame.get().players[currentPlayer].getAI() != null)
                    {
                        if (board.selectedId == int.MinValue) // Select Piece
                        {
                            if (rc.pos == -100)
                            {
                                cyclePlayer();
                            }
                            else if (isSelectValid(rc.pos, LotusGame.get().players[currentPlayer]))
                            {
                                LotusGame.get().FireEvent(new GameEvent.Select(rc.pos));
                            }
                            else//Deselect
                            {
                                LotusGame.get().FireEvent(new GameEvent.Select(int.MinValue));
                            }
                        }
                        else if (isMoveValid(board.selectedId, rc.pos, LotusGame.get().players[currentPlayer])) // Move Piece
                        {
                            LotusGame.get().FireEvent(new GameEvent.Move(board.selectedId, rc.pos));
                        }
                        else //Deselect
                        {
                            LotusGame.get().FireEvent(new GameEvent.Select(int.MinValue));
                        }
                    }
                    break;

                case GameEvent.GameEventType.MovePiece:
                    GameEvent.Move move = (GameEvent.Move)ge;

                    board.selectedId = int.MinValue;
                    if (move.frompos != move.topos)
                    {
                        board.movePiece(move.frompos, move.topos);
                        if (LotusGame.get().net != null)
                            LotusGame.get().net.Send(ge);
                        cyclePlayer();
                    }
                    break;


                case GameEvent.GameEventType.SelectPiece:
                    GameEvent.Select select = (GameEvent.Select)ge;

                    board.selectedId = select.pos;
                    if(LotusGame.get().net != null)
                        LotusGame.get().net.Send(ge);
                    break;


                case GameEvent.GameEventType.GameOver:
                    GameEvent.GameOver gameover = (GameEvent.GameOver)ge;

                    if (LotusGame.get().net != null)
                        LotusGame.get().net.Send(ge);
                    break;
                
                case GameEvent.GameEventType.AITurn:
                    GameEvent.AITurn aiturn = (GameEvent.AITurn) ge;
                    LotusGame.get().players[aiturn.ai].getAI().doMove(LotusGame.get().players[aiturn.ai], board);
                    break;
                
                case GameEvent.GameEventType.SetPlayer:
                    GameEvent.ChangePlayer playerchange = (GameEvent.ChangePlayer)ge;
                    LotusGame.get().setCurrentPlayer(playerchange.player);

                    if(LotusGame.get().net != null)
                        LotusGame.get().net.Send(ge);
                    break;
            }
        }

        private void cyclePlayer()
        {
            for (int i = 0; i < LotusGame.get().players.Length; i++)
            {
                if (LotusGame.get().players[i].getAI() != null)
                    LotusGame.get().players[i].getAI().onBoardChange(LotusGame.get().players[i], Board.get());
            }

            currentPlayer = (currentPlayer + 1) % LotusGame.get().players.Length;
            while (LotusGame.get().players[currentPlayer].finished)
                currentPlayer = (currentPlayer + 1) % LotusGame.get().players.Length;
            Console.WriteLine(LotusGame.get().players[currentPlayer].color);
            int pc = Board.get().getRemainingPlayers();
            if (pc == 1)
            {
                LotusGame.get().FireEvent(new GameEvent.GameOver(currentPlayer));
                return;
            }

            LotusGame.get().FireEvent(new GameEvent.ChangePlayer(currentPlayer));
            
            if (LotusGame.get().players[currentPlayer].getAI() != null)
            {
                LotusGame.get().ScheduleEvent(new GameEvent.AITurn(currentPlayer), 0.0f);
            }
        }

        private bool isSelectValid(int select, Player p)
        {
            List<Player> starttile = Board.get().getTile(select);
            if (starttile.Count > 0 && starttile[starttile.Count - 1] == p || !canMove(p))
            {
                return true;
            }
            return false;
        }
        private bool canMove(Player p)
        {
            for (int i = 0; i < Board.get().startTiles.Length + Board.get().gameTiles.Length - 1; i++)
            {
                List<Player> tile = Board.get().getTile(i);
                if (tile.Count != 0 && tile[tile.Count - 1] == p)
                    return true;
            }
            return false;
        }

        private bool isMoveValid(int start, int end, Player p)
        {
            List<Player> starttile = Board.get().getTile(start);
            start -= Board.get().startTiles.Length;
            end -= Board.get().startTiles.Length;
            if (starttile[starttile.Count - 1] == p || !canMove(p))
            {
                int dist = starttile.Count;
                if (start < 0)
                {
                    if (dist < 4)
                        if (dist - 1 == end)
                            return true;
                    if (dist + 2 == end)
                        return true;
                }
                else
                {
                    if (start < 3 && start + dist > 2)
                        dist += 3;
                    if (end == Math.Min(start + dist, Board.get().gameTiles.Length - 1))
                        return true;
                }
            }
            return false;
        }
    }
}
