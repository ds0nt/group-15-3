using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.Graphics
{
    class GraphicsFacade
    {
        LotusWindow window;

        public delegate void UpdateEventHandler();
        public event UpdateEventHandler onUpdate;
        public delegate void DrawEventHandler();
        public event DrawEventHandler onDraw;

        public GraphicsFacade()
        {   
            window = new LotusWindow();
            window.onUpdate += new LotusWindow.UpdateEventHandler(processUpdate);
            window.onDraw += new LotusWindow.DrawEventHandler(processDraw);
        }

        public void Init()
        {
            window.Init();
            window.Load();
        }

        public void Run()
        {
            window.Run();
        }

        public void renderBoard()
        {
            
        }

        public void DrawPiece(System.Drawing.Color c, float x, float y, int layer)
        {
            Piece.Draw(new OpenTK.Graphics.Color4(c.R, c.G, c.B, 1), new OpenTK.Vector2(x, y), layer);
        }

        public void DrawBoard()
        {
            Board.Draw();
        }

        private void processUpdate()
        {
            if (onUpdate != null)
                onUpdate();
        }
        private void processDraw()
        {
            if (onDraw != null)
                onDraw();
        }
    }
}
