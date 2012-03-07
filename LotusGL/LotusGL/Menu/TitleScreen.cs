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
        LobbyData lobby;

        public TitleScreen()
        {
            lobby = new LobbyData();
            enterip = new EnterIP();
            chat = new Chat();
        }
        
        public void handleInput(char x)
        {
            if (!enterip.inputmode)
            {
                if (enterip.address != "")
                {
                    LotusGame.get().net = new Network.Client();
                    if (((Network.Client)LotusGame.get().net).Connect(enterip.address))
                    {
                        LotusGame.get().manager = new RemoteManager();
                        LotusGame.get().FireEvent(new GameEvent.SetName(LotusGame.get().name));
                    }
                    enterip.address = "";
                }
                chat.handleInput(x);
            }
            else
                enterip.handleInput(x);

        }


        public void handleRegionClick(int regionid)
        {
            //Join Activate!
            if (regionid == 100)
            {
                enterip.inputmode = true;
            } 

            // Server Activate!
            if (regionid == 101)
            {
                server = true;
                LotusGame.get().net = new Network.Server();
                ((Network.Server)LotusGame.get().net).StartListen();
                LotusGame.get().manager = new LocalManager();
            }


            //Game Activate!
            if (regionid == 1)
            {
                if (LotusGame.get().manager != null)
                {
                    if (server)
                    {
                        //Server
                        ((Network.Server)LotusGame.get().net).EndListen();

                        LotusGame.get().FireEvent(new GameEvent.GameStart(lobby.createPlayers()));
                        Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.BOARD;
                    }
                }
                else
                {
                    //Single Player
                    LotusGame.get().manager = new LocalManager();

                    LotusGame.get().FireEvent(new GameEvent.GameStart(lobby.createPlayers()));
                    Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.BOARD;
                }
                
                //Client doesnt get to :p
            }
            if (LotusGame.get().manager == null || server == true)
            {
                if (regionid == 2)
                {
                    lobby.pnext(0);
                }
                else if (regionid == 3)
                {
                    lobby.pnext(1);
                }
                else if (regionid == 4)
                {
                    lobby.pnext(2);
                }
                else if (regionid == 5)
                {
                    lobby.pnext(3);
                }
                if (LotusGame.get().manager != null)
                    LotusGame.get().FireEvent(new GameEvent.UpdateLobby(lobby));
            }
        }

        public void AddName(string name)
        {
            lobby.AddName(name);
            if (LotusGame.get().manager != null)
                LotusGame.get().FireEvent(new GameEvent.UpdateLobby(lobby));
        }

        public void SetLobby(LobbyData lobby)
        {
            this.lobby = lobby;
        }

        public void Chat(string msg)
        {
            chat.addMessage(msg);
        }

        public void Draw(Graphics.GraphicsFacade graphics)
        {
            graphics.DrawTitle();
            graphics.DrawLogo();
            graphics.DrawIP();
            graphics.DrawFinish();

            lobby.Draw(graphics);
            enterip.Draw(graphics);
            chat.Draw(graphics);
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
                new GraphicsFacade.BoardRegion2D(101, 400, 400, 100, 100), // Server
            };
            return ret;
        }
       
    }
}
