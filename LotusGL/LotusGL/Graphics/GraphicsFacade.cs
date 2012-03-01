using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.Graphics
{
    class GraphicsFacade
    {
        public enum Mode { MENU, BOARD };
        public static Mode mode;

        public struct MouseEvent
        {
            public int x;
            public int y;
            public int regionId;
        }

        public struct BoardRegion
        {
            public BoardRegion(int id, float x, float y, int height)
            {
                this.id = id;
                this.x = x;
                this.y = y;
                this.height = height;
            }
            public int id;
            public float x;
            public float y;
            public int height;
        }

        public struct BoardRegion2D
        {
            public int id;
            public float x, y, width, height;
            public BoardRegion2D(int id, float x, float y, float width, float height)
            {
                this.id = id;
                this.x = x;
                this.y = y;
                this.width = width;
                this.height = height;
            }
        }

        LotusWindow window;

        public delegate void UpdateEventHandler(MouseEvent m);
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

        public void setMode(Mode mode)
        {
            Graphics.GraphicsFacade.mode = mode;
        }

        public void Run()
        {
            window.Run();
        }

        public void DrawTitle()
        {
            if (mode == Mode.MENU)
            {
                Menu.Draw("title");
            }
        }

        public void DrawGameOver()
        {
            if (mode == Mode.MENU)
            {
                Menu.Draw("gameover");
            }
        }

        public void DrawPiece(System.Drawing.Color c, float x, float y, int layer)
        {
            if(mode == Mode.BOARD)
                Piece.Draw(new OpenTK.Graphics.Color4(c.R, c.G, c.B, 1), new OpenTK.Vector2(x, y), layer);
        }

        public void DrawBoard()
        {
            if (mode == Mode.BOARD)
                Board.Draw();
        }
        
        public void setClickableRegions(BoardRegion[] regions)
        {
            window.regions = regions;
        }
        public void setClickableRegions(BoardRegion2D[] regions)
        {
            window.regions2D = regions;
        }

        private void processUpdate(MouseEvent m)
        {
            if (onUpdate != null)
                onUpdate(m);
        }
        private void processDraw()
        {
            if (onDraw != null)
                onDraw();
        }
    }
}
