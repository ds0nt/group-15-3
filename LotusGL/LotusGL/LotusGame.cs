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
        static LotusGame me;
        GraphicsFacade graphics;
        GameManager manager;
        Board board;
        public Player[] players;

        MenuType menu = MenuType.Title;
        TitleScreen title;
        GameOver gameOver;
        List<ScheduledEvent> scheduledEvents;

        static double lastTime = 0;

        public LotusGame(GraphicsFacade graphics)
        {
            me = this;
            this.graphics = graphics;
            menu = MenuType.Title;
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

        enum MenuType
        {
            None,
            Title,
            GameOver
        };

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

            title = new TitleScreen();
            gameOver = new GameOver();
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
                if (m.regionId == 1)
                    Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.BOARD;
                switch (menu)
                {
                    case MenuType.Title:
                        graphics.setClickableRegions(title.getRegions());
                        break;
                    case MenuType.GameOver:
                        graphics.setClickableRegions(gameOver.getRegions());
                        break;
                }
            }
            else
            {
                if (m.regionId >= 0)
                    FireEvent(new GameEvent.RegionClick(m.regionId, players[((LocalManager)manager).getCurrentPlayerID()]));

                graphics.setClickableRegions(board.getRegions());
            }

            lastTime += time;
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
