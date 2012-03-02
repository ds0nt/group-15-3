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

        int currentPlayer;
        public int getCurrentPlayerID()
        {
            return currentPlayer;
        }

        public LocalManager(Board b, LotusGame g)
        {
            board = b;
            game = g;
        }

        private void cyclePlayer()
        {
            currentPlayer = (currentPlayer + 1) % game.players.Length;
            while (game.players[currentPlayer].finished)
                currentPlayer = (currentPlayer + 1) % game.players.Length;
            Console.WriteLine(game.players[currentPlayer].color);   
            int pc = board.getRemainingPlayers();
            if (pc == 1)
                game.FireEvent(new GameEvent.GameOver(currentPlayer));
        }
        
        public void onGameEvent(GameEvent.GameEvent ge)
        {
            switch (ge.type)
            {
                case GameEvent.GameEventType.RegionClick:
                    GameEvent.RegionClick rc = (GameEvent.RegionClick)ge;
                    if (rc.player == game.players[currentPlayer])
                    {
                        if (board.selectedId == int.MinValue)
                            game.FireEvent(new GameEvent.Select(rc.pos));
                        else if (board.selectedId == rc.pos)
                            game.FireEvent(new GameEvent.Select(int.MinValue));
                        else if (isValidMove(board.selectedId, rc.pos, rc.player))
                            game.FireEvent(new GameEvent.Move(board.selectedId, rc.pos));
                    }
                    break;

                case GameEvent.GameEventType.MovePiece:
                    GameEvent.Move move = (GameEvent.Move)ge;

                    board.selectedId = int.MinValue;
                    if(move.frompos != move.topos)
                        board.movePiece(move.frompos, move.topos);
                    cyclePlayer();
                    break;


                case GameEvent.GameEventType.SelectPiece:
                    GameEvent.Select select = (GameEvent.Select)ge;

                    board.selectedId = select.pos;
                    break;
                

                case GameEvent.GameEventType.GameOver:
                    GameEvent.GameOver gameover = (GameEvent.GameOver)ge;
                    

                    break;
            }
        }

        private bool isValidSelect(int select, Player p)
        {
            return false;
        }

        private bool isValidMove(int start, int end, Player p)
        {
            List<Player> starttile = board.getTile(start);
            if (starttile[starttile.Count-1] == p)
            {
                return true;
            }
            return false;
        }

    }
}
