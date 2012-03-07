using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI
{
    class AIMoves //This class has all of the moves that AI can take. THIS WILL B VERY CONVINIENT!
    {


        /////////////////////////////General Moves////////////////////////////////

        //random Move
        public void moveRandom(Player p, Board b) // do random
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b); // using AI calc to get the possible moves.
            if (moves.Count == 0)
            {
                Console.WriteLine("At MoveRandom");
                moveRandomOpponentsPiece(p, b);
            }
            else
            {
                int moveid = AICalc.rand.Next(0, moves.Count - 1); // rand means it randers!
                Console.WriteLine("AIMoves . Function Random move called!!!");
                Console.WriteLine("Move I make: " + moves[moveid].start + " to " + moves[moveid].end);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].start), 0.1f);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].end), 0.2f);
            }
        }
        //move the highest one!
        public void moveHighest(Player p, Board b)
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b); // using AI calc to get the possible moves.
            if (moves.Count == 0)
            {
                Console.WriteLine("At MoveHighest");
                moveRandomOpponentsPiece(p, b);
            }
            else
            {
                Move highest = moves[0]; //First we assume that the very first move has the highest
                List<Move> sameHeight = new List<Move>();//this for randomizing to pick the pieces that are havin same hight randomly
                sameHeight.Add(highest);

                for (int i = 1; i < moves.Count; i++)
                {
                    if (AICalc.distance(moves[i],b) > AICalc.distance(highest,b)) //replace the preivous highest with current highest.
                    {
                        highest = moves[i];
                        sameHeight = new List<Move>(); //reset the sameHight array.
                        sameHeight.Add(highest);
                    }
                    else if (AICalc.distance(moves[i], b) == AICalc.distance(highest, b)) //there are same Height guys!!
                    {
                        sameHeight.Add(moves[i]);
                        Console.WriteLine("one more piece added");
                    }
                }
                Console.WriteLine("AIMoves . Function highest move called!!!");///////////////////////////////test
                if (sameHeight.Count == 1)
                {
                    
                    Console.WriteLine("There was only one highest: "+ highest.start + " to " + highest.end);
                    LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(highest.start), 0.1f);
                    LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(highest.end), 0.2f);
                }
                else
                {
                    int ran = AICalc.rand.Next(0, sameHeight.Count);
                    Console.WriteLine(sameHeight.Count + " number of highest, choosed :"+ sameHeight[ran].start + " to " + sameHeight[ran].end);
                    LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(sameHeight[ran].start), 0.1f);
                    LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(sameHeight[ran].end), 0.2f);
                }
            }
        }
        //move the one that's close to the goal.
        public void moveClosestToGoal(Player p, Board b)
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b); // using AI calc to get the possible moves.
            if (moves.Count == 0)
            {
                Console.WriteLine("At MoveCloseToGoal");
                moveRandomOpponentsPiece(p, b);
            }
            else
            {   
                List<Move> twoDirectionHandle = new List<Move>();
                List<Move> startPosition = new List<Move>(); //just in case!
                Move closeToGoal = moves[0];
                if (closeToGoal.start < 18 && closeToGoal.start > 11)
                {
                    twoDirectionHandle.Add(closeToGoal);
                }
                if (closeToGoal.start < 12)
                {
                    startPosition.Add(closeToGoal);
                }

                //rest of it
                for (int i = 1; i < moves.Count; i++)
                {
                    if (((moves[i].start < 15 && moves[i].start > 11) && (closeToGoal.start < 18 && closeToGoal.start > 14)) || ((closeToGoal.start < 15 && closeToGoal.start > 11) && (moves[i].start < 18 && moves[i].start > 14)))
                    {
                        //There is some piece that's further than start. so i delete the start!!
                        startPosition = new List<Move>();

                        int newCloseToGoalStart=0;
                        int newMovesStart=0;
                        switch (closeToGoal.start)
                        {
                            case 12:
                                newCloseToGoalStart = 12;
                                break;
                            case 13:
                                newCloseToGoalStart = 13;
                                break;
                            case 14:
                                newCloseToGoalStart = 14;
                                break;
                            case 15:
                                newCloseToGoalStart = 12;
                                break;
                            case 16:
                                newCloseToGoalStart = 13;
                                break;
                            case 17:
                                newCloseToGoalStart = 14;
                                break;
                        }
                        switch (moves[i].start)
                        {
                            case 12:
                                newMovesStart = 12;
                                break;
                            case 13:
                                newMovesStart = 13;
                                break;
                            case 14:
                                newMovesStart = 14;
                                break;
                            case 15:
                                newMovesStart = 12;
                                break;
                            case 16:
                                newMovesStart = 13;
                                break;
                            case 17:
                                newMovesStart = 14;
                                break;
                        }
                        if (newCloseToGoalStart < newMovesStart)
                        {
                            closeToGoal = moves[i];
                            //if (closeToGoal.start < 18 && closeToGoal.start > 11)
                            //{
                            twoDirectionHandle = new List<Move>();
                            twoDirectionHandle.Add(closeToGoal);
                            //}
                        }
                        else if (newCloseToGoalStart == newMovesStart)
                        {
                            twoDirectionHandle.Add(closeToGoal);
                        }
                    }
                       

                    else if (closeToGoal.start < moves[i].start)
                    {
                        closeToGoal = moves[i];
                        if (closeToGoal.start < 12)
                        {
                            startPosition.Add(moves[i]);
                        }
                        else
                        {
                            startPosition = new List<Move>();
                        }


                        if (closeToGoal.start < 18 && closeToGoal.start> 11)
                        {
                            twoDirectionHandle = new List<Move>();
                            twoDirectionHandle.Add(closeToGoal);
                        }
                    }
                }





                if (startPosition.Count != 0) //means there's no piece gone further than start point!!!
                {
                    int ran = AICalc.rand.Next(0, twoDirectionHandle.Count);
                    Console.WriteLine(startPosition.Count + " " + ran);
                    Console.WriteLine(startPosition.Count + " number of piceces closeTogoal , choosed :" + startPosition[ran].start + " to " + startPosition[ran].end);
                    LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(startPosition[ran].start), 0.1f);
                    LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(startPosition[ran].end), 0.2f);
                        
                }
                else
                {
                    if (closeToGoal.start > 17)
                    {

                        Console.WriteLine("There was only one close To goal: " + closeToGoal.start + " to " + closeToGoal.end);
                        LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(closeToGoal.start), 0.1f);
                        LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(closeToGoal.end), 0.2f);
                    }
                    else
                    {
                        try
                        {
                            int ran = AICalc.rand.Next(0, twoDirectionHandle.Count);
                            Console.WriteLine(twoDirectionHandle.Count + " " + ran);
                            Console.WriteLine(twoDirectionHandle.Count + " number of piceces closeTogoal , choosed :" + twoDirectionHandle[ran].start + " to " + twoDirectionHandle[ran].end);
                            LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(twoDirectionHandle[ran].start), 0.1f);
                            LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(twoDirectionHandle[ran].end), 0.2f);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
        //move the one that's close to the start position.
        public void moveStartPosition(Player p, Board b)
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b); // using AI calc to get the possible moves.
            if (moves.Count == 0)
            {
                Console.WriteLine("At MoveCloseToStart");
                moveRandomOpponentsPiece(p, b);
            }
            else
            {
                List<Move> twoDirectionHandle = new List<Move>();
                List<Move> startPosition = new List<Move>();
                Move closeToStart = moves[0];
                if (closeToStart.start < 18 && closeToStart.start > 11)
                {
                    twoDirectionHandle.Add(closeToStart);
                }
                if (closeToStart.start < 12)
                {
                    startPosition.Add(closeToStart);
                }

                //rest of stuff!
                for (int i = 1; i < moves.Count; i++)
                {
                    if (((moves[i].start < 15 && moves[i].start > 11) && (closeToStart.start < 18 && closeToStart.start > 14)) || ((closeToStart.start < 15 && closeToStart.start > 11) && (moves[i].start < 18 && moves[i].start > 14)))
                    {
                        int newcloseToStartStart = 0;
                        int newMovesStart = 0;
                        switch (closeToStart.start)
                        {
                            case 12:
                                newcloseToStartStart = 12;
                                break;
                            case 13:
                                newcloseToStartStart = 13;
                                break;
                            case 14:
                                newcloseToStartStart = 14;
                                break;
                            case 15:
                                newcloseToStartStart = 12;
                                break;
                            case 16:
                                newcloseToStartStart = 13;
                                break;
                            case 17:
                                newcloseToStartStart = 14;
                                break;
                        }
                        switch (moves[i].start)
                        {
                            case 12:
                                newMovesStart = 12;
                                break;
                            case 13:
                                newMovesStart = 13;
                                break;
                            case 14:
                                newMovesStart = 14;
                                break;
                            case 15:
                                newMovesStart = 12;
                                break;
                            case 16:
                                newMovesStart = 13;
                                break;
                            case 17:
                                newMovesStart = 14;
                                break;
                        }
                        if (newcloseToStartStart > newMovesStart)
                        {
                            closeToStart = moves[i];
                           
                            twoDirectionHandle = new List<Move>();
                            twoDirectionHandle.Add(closeToStart);
                        }
                        else if (newcloseToStartStart == newMovesStart)
                        {
                            twoDirectionHandle.Add(closeToStart);
                        }
                    }


                    else if (closeToStart.start > moves[i].start)
                    {
                        if(closeToStart.start < 11 && moves[i].start < 11)
                        {
                            startPosition.Add(moves[i]);
                        }   
                        else
                        {
                            closeToStart = moves[i];
                            if (closeToStart.start < 18 && closeToStart.start > 11)
                            {
                                twoDirectionHandle = new List<Move>();
                                twoDirectionHandle.Add(closeToStart);
                            }
                            else if(closeToStart.start < 12) 
                            {
                                startPosition.Add(closeToStart);
                            }
                        }
                    }
                }






                if (startPosition.Count != 0)
                {
                    int ran = AICalc.rand.Next(0, startPosition.Count);
                    Console.WriteLine(startPosition.Count + " number of piceces at startPosition , choosed :" + startPosition[ran].start + " to " + startPosition[ran].end);
                    LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(startPosition[ran].start), 0.1f);
                    LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(startPosition[ran].end), 0.2f);
                }
                else
                {
                    if (closeToStart.start > 17)
                    {

                        Console.WriteLine("There was only one close To Start: " + closeToStart.start + " to " + closeToStart.end);
                        LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(closeToStart.start), 0.1f);
                        LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(closeToStart.end), 0.2f);
                    }
                    else
                    {
                        int ran = AICalc.rand.Next(0, twoDirectionHandle.Count);
                        Console.WriteLine(twoDirectionHandle.Count + " number of piceces closeToStart , choosed :" + twoDirectionHandle[ran].start + " to " + twoDirectionHandle[ran].end);
                        LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(twoDirectionHandle[ran].start), 0.1f);
                        LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(twoDirectionHandle[ran].end), 0.2f);
                    }
                }
            }
        }
        public void coverOpponent(Player p, Board b)
        {
            //dist = b.getTile(m.start).Count;
        }
        /* New rule should follow following format:
        public void moveNew(Player p, Board b)
        {
            List<Move> moves = AICalc.getPossibleMoves(p, b); // using AI calc to get the possible moves.
            if (moves.Count == 0)
            {
                moveRandomOpponentsPiece(p, b);
            }
            else
            {
                ----------Coding
                int moveid = AICalc.rand.Next(0, moves.Count - 1); // rand means it randers!
                ----------coding
                Console.WriteLine("AIMoves . Function New move called!!!");
                Console.WriteLine("Move I make: " + moves[moveid].start + " to " + moves[moveid].end);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].start, 0.1f);
                LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].end, 0.2f);
            }
        }
        */




        ///////////////////////Has no Move Handler////////////////////////////
        public void moveRandomOpponentsPiece(Player p, Board b)
        {
            Console.WriteLine(p.name + " Has No Moves!"); // if this guy has no move
            bool moveMade = false;
            int count = 0;
            while (moveMade == false)
            {
                int i = AICalc.rand.Next(0, LotusGame.get().players.Length);
      
                if (LotusGame.get().players[i] != p)
                {
                    List<Move> moves = AICalc.getPossibleMoves(LotusGame.get().players[i], b);
                    if (moves.Count != 0)
                    {
                        int moveid = AICalc.rand.Next(0, moves.Count - 1);
                        moveMade = true;
                        Console.WriteLine("Move I make: "+ "player " + i +", " + moves[moveid].start + " to " + moves[moveid].end);
                        LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].start), 0.1f);
                        LotusGame.get().ScheduleEvent(new GameEvent.RegionClick(moves[moveid].end), 0.2f);
                    }
                }
                Console.WriteLine(count);
            }
        }
    }
}
