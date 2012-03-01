using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace LotusGL
{
    class Player
    {
        public Color color;
        public string name;
        public bool finished;
        public Player(Color color, string name)
        {
            finished = false;
            this.name = name;
            this.color = color;
        }
    }
}
