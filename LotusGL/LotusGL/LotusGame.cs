using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL
{
    class LotusGame
    {
        Graphics.GraphicsFacade graphics;
        public LotusGame(Graphics.GraphicsFacade graphics)
        {
            this.graphics = graphics;
            graphics.Init();
            graphics.onUpdate += new Graphics.GraphicsFacade.UpdateEventHandler(this.Update);
            graphics.onDraw += new Graphics.GraphicsFacade.DrawEventHandler(this.Draw);
            graphics.Run();
        }

        public void Update()
        {

        }

        public void Draw()
        {
            graphics.DrawPiece(System.Drawing.Color.RosyBrown, 256, 256, 3);
            graphics.DrawBoard();
        }
    }
}
