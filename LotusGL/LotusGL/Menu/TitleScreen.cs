using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotusGL.Graphics;

namespace LotusGL.Menu
{
    class TitleScreen : Menu
    {   
        EnterIP enterip;
        Chat chat;
        bool server = false;


        public TitleScreen()
        {
            enterip = new EnterIP();
            chat = new Chat();
        }
        
        enum MenuState
        {
            MainMenu,
            PlayerMenu,
            NetworkMenu
        }

        public void handleInput(char x)
        {
            if (!enterip.inputmode)
            {
                if (enterip.address != "")
                {
                    LotusGame.get().net = new Network.Client();
                    ((Network.Client)LotusGame.get().net).Connect(enterip.address);
                    LotusGame.get().manager = new RemoteManager();
                    enterip.address = "";
                }
                chat.handleInput(x);
            }
            else
                enterip.handleInput(x);

        }

        public void handleRegionClick(int regionid)
        {
            if (regionid == 100)
            {
                enterip.inputmode = true;
            } 
            if (regionid == 101)
            {
                server = true;
                LotusGame.get().net = new Network.Server();
                ((Network.Server)LotusGame.get().net).StartListen();
                LotusGame.get().manager = new LocalManager();
            }

            if (regionid == 1)
            {
                if (LotusGame.get().manager != null)
                {
                    if (server)
                    {
                        ((Network.Server)LotusGame.get().net).EndListen();

                        Player[] players = new Player[4];

                        players[0] = new Player(System.Drawing.Color.Red, "Red");
                        players[1] = new Player(System.Drawing.Color.Gray, "Black");
                        players[2] = new Player(System.Drawing.Color.White, "White");
                        players[3] = new Player(System.Drawing.Color.Blue, "Blue");

                        LotusGame.get().FireEvent(new GameEvent.GameStart(players));
                    }
                }
                else
                {
                    LotusGame.get().manager = new LocalManager();

                    Player[] players = new Player[4];

                    players[0] = new Player(System.Drawing.Color.Red, "Red");
                    players[1] = new Player(System.Drawing.Color.Gray, "Black");
                    players[2] = new Player(System.Drawing.Color.White, "White");
                    players[3] = new Player(System.Drawing.Color.Blue, "Blue");

                    LotusGame.get().FireEvent(new GameEvent.GameStart(players));
                }
            }
        }

        public void Draw(Graphics.GraphicsFacade graphics)
        {
            graphics.DrawTitle();
            enterip.Draw(graphics);
            chat.Draw(graphics);
        }

        public GraphicsFacade.BoardRegion2D[] getRegions()
        {
            GraphicsFacade.BoardRegion2D[] ret = new GraphicsFacade.BoardRegion2D[]
            {
                new GraphicsFacade.BoardRegion2D(1, 128, 256, 256, 128),

                new GraphicsFacade.BoardRegion2D(100, 10, 10, 100, 100), // Client
                new GraphicsFacade.BoardRegion2D(101, 400, 400, 100, 100), // Server
            };
            return ret;
        }
    }
}