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

        public delegate void UpdateEventHandler(MouseEvent m, double time);
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
                DrawMenu("title");
            }
        }
        public void DrawPlayer1()
        {
            if (mode == Mode.MENU)
            {
                DrawMenu("player1",130,260,125,60);
            }
        }
        public void DrawPlayer2()
        {
            if (mode == Mode.MENU)
            {
                DrawMenu("player2", 256, 260,125,60);
            }
        }
        public void DrawPlayer3()
        {
            if (mode == Mode.MENU)
            {
                DrawMenu("player3", 130,320, 125, 60);
            }
        }
        public void DrawPlayer4()
        {
            if (mode == Mode.MENU)
            {
                DrawMenu("player4", 256, 320, 125, 60);
            }
        }
        public void DrawLogo()
        {
            if (mode == Mode.MENU)
            {
                DrawMenu("logo",60, 10, 400, 250);
            }
        }


        public void DrawRuleAI(float x, float y, float width, float height)
        {
            if (mode == Mode.MENU)
            {
                DrawMenu("ruleAI", x, y, width, height);
            }
        }
        public void DrawStateAI(float x, float y, float width, float height)
        {
            if (mode == Mode.MENU)
            {
                DrawMenu("stateAI", x, y, width, height);
            }
        }
        public void DrawHuman(float x, float y, float width, float height)
        {
            if (mode == Mode.MENU)
            {
                DrawMenu("human", x, y, width, height);
            }
        }
        public void DrawNoPlayer(float x, float y, float width, float height)
        {
            if (mode == Mode.MENU)
            {
                DrawMenu("noPlayer", x, y, width, height);
            }
        }
        public void DrawFinish()
        {
            if (mode == Mode.MENU)
            {
                DrawMenu("finish", 224, 400, 64, 64);
            }
        }
        public void DrawGameOver()
        {
            if (mode == Mode.MENU)
            {
                Menu.Draw("gameover");
            }
        }

        private void DrawMenu(string resource)
        {
            DrawMenu(resource, 0, 0, 512, 512);
        }
        private void DrawMenu(string resource, float x, float y, float width, float height)
        {
            x = x / 256 - 1;
            y = (256 - y) / 256;// -1;
            width = width / 256;
            height = height / 256;
            Menu.Draw(resource, x, y, width, height);
        }

        public void DrawPiece(System.Drawing.Color c, float x, float y, int layer)
        {
            if(mode == Mode.BOARD)
                Piece.Draw(new OpenTK.Graphics.Color4(c.R, c.G, c.B, 1), new OpenTK.Vector2(x, y), layer);
        }

        public void DrawText(System.Drawing.Color c, float x, float y, string str)
        {
            x = (x / 256) - 1f;
            y = ((256 - y) / 256);// -1f;
            Text.Draw(new OpenTK.Graphics.Color4(c.R, c.G, c.B, 1), x, y, str);
        }

        public void DrawSelected(float x, float y, int layer)
        {
            if (mode == Mode.BOARD)
                Piece.DrawSelected(new OpenTK.Vector2(x, y), layer);
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
            for (int i = 0; i < regions.Length; i++)
            {
                regions[i].x = regions[i].x / 256 - 1;
                regions[i].y = regions[i].y / 256 - 1;
                regions[i].width = regions[i].width / 256;
                regions[i].height = regions[i].height / 256;
            }

            window.regions2D = regions;
        }

        private void processUpdate(MouseEvent m, double time)
        {
            if (onUpdate != null)
                onUpdate(m, time);
        }
        private void processDraw()
        {
            if (onDraw != null)
                onDraw();
        }
    }
}
