using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotusGL.Graphics;
namespace LotusGL
{
    class GameOver
    {
        public void Draw(Graphics.GraphicsFacade graphics)
        {
            graphics.DrawGameOver();
        }

        public GraphicsFacade.BoardRegion2D[] getRegions()
        {
            GraphicsFacade.BoardRegion2D[] ret = new GraphicsFacade.BoardRegion2D[]
            {
                new GraphicsFacade.BoardRegion2D(1, -0.5f, 0f, 1f, 0.5f)
            };

            return ret;
        }
    }
}
