using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotusGL.Graphics;

namespace LotusGL.Menu
{
    class TitleScreen : Menu
    {
        GameManager manager;
        Network.Network net;

        enum MenuState
        {
            MainMenu,
            PlayerMenu,
            NetworkMenu
        }

        public void handleRegionClick(int regionid)
        {
            if (regionid == 100)

            if (regionid == 0)
            {

                Console.WriteLine("Server/Client/Single (1/2/3)");
                string x = Console.ReadLine();



                Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.MENU;

                if (x.Equals("1"))
                {
                    net = new Network.Server();
                    ((Network.Server)net).StartListen();
                    manager = new LocalManager(Board.get());
                    while (((Network.Server)net).streams.Count < 3)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    ((Network.Server)net).EndListen();
                }
                else if (x.Equals("2"))
                {
                    net = new Network.Client();
                    Console.WriteLine("Enter Address:");
                    string y = Console.ReadLine();
                    ((Network.Client)net).Connect(y);
                    manager = new RemoteManager(Board.get());
                }
                else
                {
                    manager = new LocalManager(Board.get());
                }
            }
            if (regionid == 1)
            {
                
                
                Player[] players = new Player[4];
                players[0] = new Player(System.Drawing.Color.Red, "Red");
                //players[0].setAI(new AI.RuleStrategy());
                players[1] = new Player(System.Drawing.Color.Gray, "Black");
                //players[1].setAI(new AI.RuleStrategy());
                players[2] = new Player(System.Drawing.Color.White, "White");
                //players[2].setAI(new AI.RuleStrategy());
                players[3] = new Player(System.Drawing.Color.Blue, "Blue");
                //players[3].setAI(new AI.RuleStrategy());

                new Board(players);
                manager = new LocalManager(Board.get());
                LotusGame.get().LaunchGame(players, net, manager);

            }
        }

        public void Draw(Graphics.GraphicsFacade graphics)
        {
            graphics.DrawTitle();
        }

        public GraphicsFacade.BoardRegion2D[] getRegions()
        {
            GraphicsFacade.BoardRegion2D[] ret = new GraphicsFacade.BoardRegion2D[]
            {
                new GraphicsFacade.BoardRegion2D(1, 128, 256, 256, 128),
            };
            return ret;
        }
    }
}