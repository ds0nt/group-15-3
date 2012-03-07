using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using LotusGL.AI;
namespace LotusGL
{
    class Player
    {
        public Color color;
        public string name;
        public bool finished;
        
        AIStrategy ai;
        
        public Player(Color color, int type, string name)
        {
            switch (type)
            {
                case 0:
                    this.name = name;
                    break;
                case 1:
                    ai = new AI.Rule.RuleStrategy();
                    this.name = "Rule AI";
                    break;
                case 2:
                    ai = new AI.State.StateStrategy();
                    this.name = "State AI";
                    break;
                case 3:
                    finished = true;
                    break;
            }
            type = 0;
            finished = false;
            this.name = name;
            this.color = color;
        }
        
        public void setAI(AIStrategy ai)
        {
            this.ai = ai;
        }
        public AIStrategy getAI()
        {
            return ai;
        }
    }
}
