using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using OpenTK.Graphics.OpenGL;

using LotusGL.Graphics;

namespace LotusGL
{
    class Program
    {
        static void Main(string[] args)
        {

            GraphicsFacade graphics = new GraphicsFacade();
            LotusGame lotus = new LotusGame(graphics);
        }
    }
}
    