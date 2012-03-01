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
        GraphicsFacade graphics;
        
        Player[] players;

        Player currentPlayer;
        GameManager manager;
        Board board;

        public LotusGame(GraphicsFacade graphics)
        {
            this.graphics = graphics;
            Start();
            graphics.Init();
            graphics.onUpdate += new GraphicsFacade.UpdateEventHandler(this.Update);
            graphics.onDraw += new GraphicsFacade.DrawEventHandler(this.Draw);
            graphics.Run();
        }

        public void Start()
        {
            players = new Player[4];
            players[0] = new Player(System.Drawing.Color.Red, "Red");
            players[1] = new Player(System.Drawing.Color.Black, "Black");
            players[2] = new Player(System.Drawing.Color.White, "White");
            players[3] = new Player(System.Drawing.Color.Blue, "Blue");

            board = new Board(this, players);
            manager = new LocalManager(board, this);

            Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.BOARD;

        }
        public void FireEvent(GameEvent.GameEvent ge)
        {
            manager.onGameEvent(ge);
        }

        public void Update(Graphics.GraphicsFacade.MouseEvent m)
        {
            if (m.regionId >= 0)
                FireEvent(new GameEvent.RegionClick(m.regionId));

            graphics.setClickableRegions(board.getRegions());
        }

        public void Draw()
        {
            if (false == true)
            {

            }
            else
            {
                board.Draw(graphics);
            }
        }
    }
}
