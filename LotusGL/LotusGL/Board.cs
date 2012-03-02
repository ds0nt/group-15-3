using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LotusGL
{
    class Board
    {
        public List<Player>[] startTiles;
        public List<Player>[] gameTiles;
        PointF[] startPoints;
        PointF[] gamePoints;
        public int selectedId;
        Player[] players;
        LotusGame game;

        public Board(LotusGame game, Player[] players)
        {
            selectedId = int.MinValue;
            this.game = game;
            this.players = players;

            startTiles = new List<Player>[12];
            gameTiles = new List<Player>[18];
            for (int i = 0; i < startTiles.Length; i++)
                startTiles[i] = new List<Player>();
            for (int i = 0; i < gameTiles.Length; i++)
                gameTiles[i] = new List<Player>();

            startPoints = new PointF[12];
            gamePoints = new PointF[18];
            
            CreateBoard();
            CreateStartLocationsTable();
            CreateLocationsTable();
            resetPlayers();

            gameTiles[3].Add(players[1]);
            gameTiles[3].Add(players[2]);
            gameTiles[3].Add(players[3]);
            gameTiles[3].Add(players[0]);
            gameTiles[5].Add(players[3]);
            gameTiles[5].Add(players[3]);
            gameTiles[5].Add(players[2]);
        }
        public int getRemainingPlayers()
        {
            int finishedCount = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].finished)
                    finishedCount++;
            }
            return finishedCount;
        }

        private void resetPlayers()
        {
            for (int i = 0; i < players.Length; i++)
            {
                players[i].finished = false;
            }
        }
        
        public List<Player> getTile(int location)
        {
            if(location >= startTiles.Length)
                return gameTiles[location - startTiles.Length];
            return startTiles[location];
        }

        public PointF getPosition(int location)
        {
            if (location >= startPoints.Length)
                return gamePoints[location - startPoints.Length];
            return startPoints[location];
        }

        public void movePiece(int x, int y)
        {
            List<Player> l = getTile(x);
            if (l.Count == 0)
                return;
            getTile(y).Add(l[l.Count - 1]);
            l.RemoveAt(l.Count - 1);
        }

        public bool hasSelected()
        {
            return selectedId >= 0;
        }

        public void CreateBoard()
        {
            if (players.Length == 2)
            {
               for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 4 - (i % 4); j++)
                    {
                        startTiles[i].Add(players[i / 4]);
                    }
                }
            }
            else if (players.Length == 3)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 3 - (i % 3); j++)
                    {
                        startTiles[i].Add(players[i / 3]);
                    }
                }
            }
            else if (players.Length == 4)
            {
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 3 - (i % 3); j++)
                    {
                        startTiles[i].Add(players[i / 3]);
                    }
                }
            }
        }

        public void Draw(Graphics.GraphicsFacade graphics)
        {
            graphics.DrawBoard();
            for (int i = 0; i < startTiles.Length; i++)
            {
                int level = 1;
                foreach (Player p in startTiles[i])
                {
                    graphics.DrawPiece(p.color, startPoints[i].X, startPoints[i].Y, level);
                    level++;
                }
            }
            for (int i = 0; i < gameTiles.Length; i++)
            {
                int level = 1;
                foreach (Player p in gameTiles[i])
                {
                    graphics.DrawPiece(p.color, gamePoints[i].X, gamePoints[i].Y, level);
                    level++;
                }
            }
            if(selectedId != int.MinValue)
            {
                PointF selpos = getPosition(selectedId);
                graphics.DrawSelected(selpos.X, selpos.Y, getTile(selectedId).Count);
            }
        }

        //Create the locations of each position on the board (the top left coordinate)
        void CreateLocationsTable()
        {
	        gamePoints[0] = new PointF(118, 198);
	        gamePoints[1] = new PointF(118, 278);
	        gamePoints[2] = new PointF(164, 343);
	        gamePoints[3] = new PointF(360, 200);
	        gamePoints[4] = new PointF(360, 278);
	        gamePoints[5] = new PointF(314, 343);
	        gamePoints[6] = new PointF(239, 368);
	        gamePoints[7] = new PointF(239, 448);
	        gamePoints[8] = new PointF(117, 408);
	        gamePoints[9] = new PointF(41, 304);
	        gamePoints[10] = new PointF(41, 176);
	        gamePoints[11] = new PointF(117, 71);
	        gamePoints[12] = new PointF(239, 32);
	        gamePoints[13] = new PointF(359, 71);
	        gamePoints[14] = new PointF(437, 176);
	        gamePoints[15] = new PointF(437, 303);
	        gamePoints[16] = new PointF(361, 408);

	        gamePoints[17] = new PointF(300, 420); //finish zone
        }

        void CreateStartLocationsTable()
        {
	        if (players.Length == 2)
	        {
		        startPoints[0] = new PointF(215, 167);
		        startPoints[1] = new PointF(167, 215);
		        startPoints[2] = new PointF(167, 263);
		        startPoints[3] = new PointF(215, 311);

		        startPoints[4] = new PointF(263, 167);
		        startPoints[5] = new PointF(311, 215);
		        startPoints[6] = new PointF(311, 263);
		        startPoints[7] = new PointF(263, 311);

		        startPoints[8] = new PointF(0, 0);
		        startPoints[9] = new PointF(0, 0);
		        startPoints[10] = new PointF(0, 0);
		        startPoints[11] = new PointF(0, 0);
	        }
	        else if (players.Length == 3)
	        {
		        startPoints[0] = new PointF(191, 191);
		        startPoints[1] = new PointF(191, 239);
		        startPoints[2] = new PointF(191, 287);

		        startPoints[3] = new PointF(239, 191);
		        startPoints[4] = new PointF(239, 239);
		        startPoints[5] = new PointF(239, 287);

		        startPoints[6] = new PointF(287, 191);
		        startPoints[7] = new PointF(287, 239);
		        startPoints[8] = new PointF(287, 287);

		        startPoints[9] = new PointF(0, 0);
		        startPoints[10] = new PointF(0, 0);
		        startPoints[11] = new PointF(0, 0);
	        }
	        else if (players.Length == 4)
	        {
		        startPoints[0] = new PointF(215, 167);
		        startPoints[1] = new PointF(167, 215);
		        startPoints[2] = new PointF(167, 263);

		        startPoints[3] = new PointF(263, 167);
		        startPoints[4] = new PointF(311, 215);
		        startPoints[5] = new PointF(311, 263);

		        startPoints[6] = new PointF(215, 215);
		        startPoints[7] = new PointF(215, 263);
		        startPoints[8] = new PointF(215, 311);

		        startPoints[9] = new PointF(263, 215);
		        startPoints[10] = new PointF(263, 263);
		        startPoints[11] = new PointF(263, 311);
	        }
        }

        public Graphics.GraphicsFacade.BoardRegion[] getRegions()
        {
            Graphics.GraphicsFacade.BoardRegion[] ret = new Graphics.GraphicsFacade.BoardRegion[startTiles.Length + gameTiles.Length];
            for (int i = 0; i < startTiles.Length; i++)
            {
                ret[i] = new Graphics.GraphicsFacade.BoardRegion(i, startPoints[i].X, startPoints[i].Y, startTiles[i].Count);
            }

            for (int i = 0; i < gameTiles.Length; i++)
            {
                ret[i+startTiles.Length] = new Graphics.GraphicsFacade.BoardRegion(i+startTiles.Length, gamePoints[i].X, gamePoints[i].Y, gameTiles[i].Count);
            }

            return ret;
        }

    }
}
