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
        /////////////// RANDOM NUMBER////////////////////// 
        public static Random rand = new Random(); //please don't define extra but just use this !!


        ////////////// Get Possible Moves//////////////////
        public static List<Move> getPossibleMoves(Player player, Board b)
        {
	        List<Move> Moves = new List<Move>(); //initialize empty moves.

	        for(int i = 0; i < b.startTiles.Length + b.gameTiles.Length - 1; i++) //for/while #of start tiles and # of game tiles
	        {
                List<Player> tile = b.getTile(i);       //gets tiles of current player's peices are currently lading on
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
                        Moves.Add(new Move(i, Math.Min(i + dist, b.startTiles.Length + b.gameTiles.Length - 1)));
			        }
		        }
	        }

	        return Moves;
        }


        ///////////////////////////Calculate the distance.//////////////////////////////
        ///you can use this ONLY AFTER you'd got the possible moves. this won't work for any random moves.
        ///this will only work for the moves that had start and end points.
        public static int distance(Move m, Board b) //simpler version of Distance. doesn't handle invalied moves.
        {
            int dist=0;
            //let's get the starting points height
            dist = b.getTile(m.start).Count;


            return dist;  
        }
    }
}
