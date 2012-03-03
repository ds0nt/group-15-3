﻿using System;
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
        GameManager manager;
        Board board;
        public Player[] players;
        
        //does not dictate player, this is what we THINK the players turn is
        //the players turn is managed by whoever owns localmanager
        private int currentPlayer;

        Menu.Menu currentMenu;
        TitleScreen title;
        Menu.GameOver gameOver;
        
        List<ScheduledEvent> scheduledEvents;

        static double lastTime = 0;

        public LotusGame(GraphicsFacade graphics)
        {
            me = this;
            this.graphics = graphics;


            title = new TitleScreen();
            gameOver = new Menu.GameOver();
            currentMenu = title;

            StartGame();
            graphics.Init();
            graphics.onUpdate += new GraphicsFacade.UpdateEventHandler(this.Update);
            graphics.onDraw += new GraphicsFacade.DrawEventHandler(this.Draw);
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

        public void StartGame()
        {
            players = new Player[4];
            players[0] = new Player(System.Drawing.Color.Red, "Red");
            players[0].setAI(new AI.RuleStrategy());
            players[1] = new Player(System.Drawing.Color.Black, "Black");
            players[1].setAI(new AI.RuleStrategy());
            players[2] = new Player(System.Drawing.Color.White, "White");
            players[2].setAI(new AI.RuleStrategy());
            players[3] = new Player(System.Drawing.Color.Blue, "Blue");
            players[3].setAI(new AI.RuleStrategy());

            board = new Board(this, players);
            manager = new LocalManager(board);
            scheduledEvents = new List<ScheduledEvent>();
            Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.MENU;
        }

        public void EndGame()
        {
            Graphics.GraphicsFacade.mode = GraphicsFacade.Mode.MENU;
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

        public void Update(Graphics.GraphicsFacade.MouseEvent m, double time)
        {
            FireScheduled();
            if (Graphics.GraphicsFacade.mode == Graphics.GraphicsFacade.Mode.MENU)
            {
                if (m.regionId >= 0)
                    currentMenu.handleRegionClick(m.regionId);
                graphics.setClickableRegions(currentMenu.getRegions());
            }
            else
            {
                if (players[currentPlayer].local)
                {
                    if (m.regionId >= 0)
                        FireEvent(new GameEvent.RegionClick(m.regionId, players[currentPlayer]));
                }

                graphics.setClickableRegions(board.getRegions());
            }

            lastTime += time;
        }

        public void Draw()
        {
            currentMenu.Draw(graphics);
            board.Draw(graphics);
        }

        public void setCurrentPlayer(int playerindex)
        {
            currentPlayer = playerindex;
        }
    }
}
