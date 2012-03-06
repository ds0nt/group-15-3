using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.AI.Rule
{
    //IT's just a rule class. It has name and weight of the move that this AI can do.
    class rule
    {
        public string name{get; set;}   //name of the rule
        public int weight{ get; set; }  //weight of the rule. This continuously changes.
        public rule(string n)
        {
            name = n;
            weight = 100;
        }
    }
    class RuleStrategy : AIStrategy
    {
        
        ////////////////////////////////RULESTRATEGY ONLY STUFF.////////////////////////////////////////////
  
        List<rule> rules { get; set; }
        public RuleStrategy()
        {
            rules = initRules();
        }
        public List<rule> initRules()
        {
            List<rule> rules = new List<rule>();
            
            rule moveRandom = new rule("moveRandom");
            rules.Add(moveRandom);
            rule moveHighest = new rule("moveHighest");
            rules.Add(moveHighest);
            rule moveClosestToGoal = new rule("moveClosestToGoal");
            rules.Add(moveClosestToGoal);
            rule moveStartPosition = new rule("moveStartPosition");
            rules.Add(moveStartPosition);
            /* New rules must be added here!!
            rule moveNew = new rule("moveNew");
            rules.Add(moveNew);
            */
            return rules;
        }
        
        //This chooses the move. There are rules that's already set up, and I pick thie rule 
        //WRT the weight of it. if the weights of rules are the same, then choose one randomly.
        public rule chooseMove()
        {
            rule moveOfThisTurn = null;  //this will be the move of this turn. 
            List<rule> sameWeight = new List<rule>(); 


            for (int i = 0; i < rules.Count; i++)
            {
                if (i == 0)
                {
                    moveOfThisTurn = rules[i];
                    sameWeight.Add(moveOfThisTurn);
                    Console.WriteLine("first random thing is current.");
                }
                else
                {
                    if (moveOfThisTurn.weight < rules[i].weight)
                    {
                        moveOfThisTurn = rules[i];
                        sameWeight = new List<rule>();
                        sameWeight.Add(moveOfThisTurn);
                    }
                    else if (moveOfThisTurn.weight == rules[i].weight)
                    {
                        //rule[] newArray = new rule[sameWeight.Length + 1];
                        //for (int j = 0; j < sameWeight.Length; j++)
                        //{
                        //    newArray[j] = sameWeight[j];
                        //}
                        //newArray[newArray.Length - 1] = rules[i];
                        //sameWeight = newArray;
                        sameWeight.Add(rules[i]);
                        Console.WriteLine("one more rule is added");
                    }
                }
            }
            if (sameWeight.Count == 1)
            {
                moveOfThisTurn.weight--;
                Console.WriteLine(moveOfThisTurn.name + "'s weight is now " + moveOfThisTurn.weight);
                return moveOfThisTurn;
            }
            else
            {
                //Random random = new Random();
                int ran = AICalc.rand.Next(0, sameWeight.Count);
                sameWeight[ran].weight--;
                Console.WriteLine(sameWeight[ran].name + "'s weight is now " + sameWeight[ran].weight);
                return sameWeight[ran];
            }
        }


        ////////////////////////////////FROM INTERFACE STUFF.////////////////////////////////////////////

        public void doMove(Player p, Board b)
        {
            rule moveOfThisTurn = chooseMove();
            AIMoves goingTo = new AIMoves();
            switch (moveOfThisTurn.name)
            {
                case "moveRandom":
                    Console.WriteLine("moveRandom");
                    goingTo.moveRandom(p,b);
                    break;
                case "moveHighest":
                    Console.WriteLine("moveHighest");
                    goingTo.moveHighest(p, b);
                    break;
                case "moveClosestToGoal":
                    Console.WriteLine("moveClosestToGoal");
                    goingTo.moveClosestToGoal(p, b);
                    break;
                case "moveStartPosition":
                    Console.WriteLine("moveStartPosition");
                    goingTo.moveStartPosition(p, b);
                    break;
                /* Newly added rules must follow this rule:
                case "moveNew":
                    Console.WriteLine("moveNew");
                    goingTo.moveNew(p, b);
                    break;
                 */
            }

        }
        public void onBoardChange(Player p, Board b)
        {
            //state strategy uses this.
        }
    }
}

