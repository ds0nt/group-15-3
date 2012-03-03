using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI
{
    struct Move
    {
        public int start, end;
        public Move(int s, int e)
        {
            start = s;
            end = e;
        }
    }

    class AICalc
    {
        
        public static List<Move> getPossibleMoves(Player player, Board b)
        {
	        List<Move> Moves = new List<Move>();

	        for(int i = 0; i < b.startTiles.Length + b.gameTiles.Length; i++)
	        {
                List<Player> tile = b.getTile(i);
		        if(tile.Count > 0 && tile[tile.Count-1] == player)
		        {
                    int dist = tile.Count;
                    if (i < b.startTiles.Length)
			        {
				        if(dist < 4)
                            Moves.Add(new Move(i, dist - 1 + b.startTiles.Length));
                        Moves.Add(new Move(i, dist + 2 + b.startTiles.Length));
			        }
			        else
			        {
                        if (i < 3 + b.startTiles.Length && i + dist > 2 + b.startTiles.Length)
					        dist += 3;
				        Moves.Add(new Move(i, i+dist));
			        }
		        }
	        }

	        return Moves;
        }
    }
}
