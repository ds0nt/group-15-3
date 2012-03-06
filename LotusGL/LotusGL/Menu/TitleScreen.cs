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

        
        int p1type = 0;
        int p2type = 0;
        int p3type = 0;
        int p4type = 0;


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
                Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.BOARD;
            }
            else if (regionid == 2)
            {
                p1type = (p1type + 1) % 4;
                
            }
             else if (regionid == 3)
            {
                p2type = (p2type + 1) % 4;
             }
             else if (regionid == 4)
            {
                 p3type = (p3type + 1) % 4;
             }
             else if(regionid == 5)
            {
                p4type = (p4type + 1) % 4;
            }
        }
       
        public void Draw(Graphics.GraphicsFacade graphics)
        {
            graphics.DrawTitle();
            graphics.DrawIP();
            graphics.DrawHost();

            if(p1type == 0)
                graphics.DrawPlayer1();
            else if(p1type == 1)
                graphics.DrawRuleAI(130, 260, 125, 60);
            else if(p1type == 2)
                graphics.DrawStateAI(130, 260, 125, 60);
            else
                graphics.DrawHuman(130, 260, 125, 60);

            if (p2type == 0)
                graphics.DrawPlayer2();
            else if (p2type == 1)
                graphics.DrawRuleAI(256, 260, 125, 60);
            else if (p2type == 2)
                graphics.DrawStateAI(256, 260, 125, 60);
            else
                graphics.DrawHuman(256, 260, 125, 60);
           
            if (p3type == 0)
                graphics.DrawPlayer3();
            else if (p3type == 1)
                graphics.DrawRuleAI(130, 320, 125, 60);
            else if (p3type == 2)
                graphics.DrawStateAI(130, 320, 125, 60);
            else 
                graphics.DrawHuman(130, 320, 125, 60);

            if (p4type == 0)
                graphics.DrawPlayer4();
            else if (p4type == 1)
                graphics.DrawRuleAI(256, 320, 125, 60);
            else if (p4type == 2)
                graphics.DrawStateAI(256, 320, 125, 60);
            else
                graphics.DrawHuman(256, 320, 125, 60);
           
            
            graphics.DrawLogo();
            enterip.Draw(graphics);
            chat.Draw(graphics);
            graphics.DrawFinish();

        }

        public GraphicsFacade.BoardRegion2D[] getRegions()
        {
            GraphicsFacade.BoardRegion2D[] ret = new GraphicsFacade.BoardRegion2D[]
            {
                new GraphicsFacade.BoardRegion2D(1, 224, 400, 64, 64), //Start button
                new GraphicsFacade.BoardRegion2D(2, 130,260,125,60),
                new GraphicsFacade.BoardRegion2D(3, 256, 260,125,60),
                new GraphicsFacade.BoardRegion2D(4, 130, 320, 125, 60),
                new GraphicsFacade.BoardRegion2D(5, 256, 320, 125, 60),
            
                new GraphicsFacade.BoardRegion2D(100, 10, 450, 125, 60), // Client
                new GraphicsFacade.BoardRegion2D(101,377, 450, 125, 60), // Server
            };
            return ret;
        }
       
    }
}
