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
        public Player(Color color, string name)
        {
            this.name = name;
            this.color = color;
        }
    }
}
