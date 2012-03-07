using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotusGL.GameEvent;
using LotusGL.Graphics;
using LotusGL.Menu;

namespace LotusGL
{
    class LotusGame
    {
        static LotusGame me;
        GraphicsFacade graphics;
        public GameManager manager;
        public Network.Network net;
        Board board;
        public Player[] players;
        
        //does not dictate player, this is what we THINK the players turn is
        //the players turn is managed by whoever owns localmanager
        public int currentPlayer;

        Menu.Menu currentMenu;
        TitleScreen title;
        List<ScheduledEvent> scheduledEvents;

        static double lastTime = 0;
        
        public string name = "";

        public LotusGame(GraphicsFacade graphics)
        {
            Console.Write("Name:");
            name = Console.ReadLine();
                        
            me = this;
            this.graphics = graphics;

            graphics.Init();
            graphics.onUpdate += new GraphicsFacade.UpdateEventHandler(this.Update);
            graphics.onDraw += new GraphicsFacade.DrawEventHandler(this.Draw);

            title = new TitleScreen();
            currentMenu = title;
            
            graphics.Run(); 
        }

        public static LotusGame get()
        {
            return me;
        }
        
        struct ScheduledEvent
        {
            public double startTime;
            public GameEvent.GameEvent ge;
        }

        
        public void LaunchGame(Player[] players)
        {
            this.players = players;
            board = Board.get();
            scheduledEvents = new List<ScheduledEvent>();

            Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.BOARD;
        }

        public void EndGame()
        {
            Graphics.GraphicsFacade.mode = GraphicsFacade.Mode.MENU;
            board = null;
            net = null;
            manager = null;
            players = null;
            scheduledEvents.Clear();
        }

        public void ScheduleEvent(GameEvent.GameEvent ge, double seconds)
        {
            ScheduledEvent se;
            se.ge = ge;
            se.startTime = lastTime + seconds;
            scheduledEvents.Add(se);
        }

        private void FireScheduled()
        {

            List<ScheduledEvent> fireables = new List<ScheduledEvent>();
            foreach(ScheduledEvent se in scheduledEvents)
            {
                if (se.startTime < lastTime)
                {
                    fireables.Add(se);
                }
            }
            foreach (ScheduledEvent se in fireables)
                FireEvent(se.ge);
            fireables.Clear();
            scheduledEvents.RemoveAll(scheduleClear);

        }
        private static bool scheduleClear(ScheduledEvent se)
        {
            return lastTime > se.startTime;
        }

        public void FireEvent(GameEvent.GameEvent ge)
        {
            manager.onGameEvent(ge);
        }

        public void AddName(string name)
        {
            title.AddName(name);
        }
        public void SetLobby(Menu.LobbyData lobby)
        {
            title.SetLobby(lobby);
        }

        public void Update(Graphics.GraphicsFacade.InputEvent m, double time)
        {

            if (Graphics.GraphicsFacade.mode == Graphics.GraphicsFacade.Mode.MENU)
            {
                title.handleInput(m.lastKey);
                if (m.regionId >= 0)
                    currentMenu.handleRegionClick(m.regionId);

                graphics.setClickableRegions(currentMenu.getRegions());
            }
            else
            {
                if (board != null)
                {
                    if (m.regionId >= 0)
                        FireEvent(new GameEvent.RegionClick(m.regionId));
                    graphics.setClickableRegions(board.getRegions());
                }
            }


            if (net != null)
            {
                GameEvent.GameEvent ge = net.Receive();
                if (ge != null)
                    FireEvent(ge);
            }

            if(scheduledEvents != null)
                FireScheduled();



            lastTime += time;
        }


        public void Draw()
        {
            if(board != null)
                board.Draw(graphics);
            if(currentMenu != null)
                currentMenu.Draw(graphics);
        }

        public void setCurrentPlayer(int playerindex)
        {
            currentPlayer = playerindex;
        }
    }
}
