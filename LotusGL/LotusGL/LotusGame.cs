using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotusGL.GameEvent;
using LotusGL.Graphics;

namespace LotusGL
{
    class LotusGame
    {
        enum MenuType
        {
            None,
            Title,
            GameOver
        };

        GraphicsFacade graphics;
        
        public Player[] players;

        GameManager manager;
        MenuType menu = MenuType.Title;
        TitleScreen title;
        GameOver gameOver;
        Board board;

        public LotusGame(GraphicsFacade graphics)
        {
            this.graphics = graphics;
            StartGame();
            graphics.Init();
            graphics.onUpdate += new GraphicsFacade.UpdateEventHandler(this.Update);
            graphics.onDraw += new GraphicsFacade.DrawEventHandler(this.Draw);
            graphics.Run();
        }



        public void StartGame()
        {
            players = new Player[4];
            players[0] = new Player(System.Drawing.Color.Red, "Red");
            players[1] = new Player(System.Drawing.Color.Black, "Black");
            players[2] = new Player(System.Drawing.Color.White, "White");
            players[3] = new Player(System.Drawing.Color.Blue, "Blue");

            title = new TitleScreen();
            gameOver = new GameOver();
            board = new Board(this, players);
            manager = new LocalManager(board, this);

            Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.MENU;
        }

        public void EndGame()
        {
            Graphics.GraphicsFacade.mode = GraphicsFacade.Mode.MENU;
        }

        public void FireEvent(GameEvent.GameEvent ge)
        {
            manager.onGameEvent(ge);
        }

        public void Update(Graphics.GraphicsFacade.MouseEvent m)
        {
            if (Graphics.GraphicsFacade.mode == Graphics.GraphicsFacade.Mode.MENU)
            {
                if (m.regionId == 1)
                    Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.BOARD;
                switch (menu)
                {
                    case MenuType.Title:
                        title.getRegions();
                        break;
                    case MenuType.GameOver:
                        gameOver.getRegions();
                        break;
                }
            }
            else
            {
                if (m.regionId >= 0)
                    FireEvent(new GameEvent.RegionClick(m.regionId));

                graphics.setClickableRegions(board.getRegions());
            }
        }

        public void Draw()
        {
            switch (menu)
            {
                case MenuType.Title:
                    title.Draw(graphics);
                    break;
                case MenuType.GameOver:
                    gameOver.Draw(graphics);
                    break;
            }

            board.Draw(graphics);
        }
    }
}
