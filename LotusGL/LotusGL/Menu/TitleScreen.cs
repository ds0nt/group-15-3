using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotusGL.Graphics;
namespace LotusGL.Menu
{   

    class TitleScreen : Menu
    {

        int p1type = 0;
        int p2type = 0;
        int p3type = 0;
        int p4type = 0;
        public void handleRegionClick(int regionid)
        {
            if (regionid == 1)
                Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.BOARD;
            else if (regionid == 2)
            {
                p1type = (p1type + 1) % 4;
                Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.MENU;
                
            }
             else if (regionid == 3)
            {
                p2type = (p2type + 1) % 4;
                Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.MENU;
             }
             else if (regionid == 4)
            {
                 p3type = (p3type + 1) % 4;
                 Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.MENU;
             }
             else if(regionid == 5)
            {
                p4type = (p4type + 1) % 4;
                 Graphics.GraphicsFacade.mode = Graphics.GraphicsFacade.Mode.MENU;
            }
        }
       
        public void Draw(Graphics.GraphicsFacade graphics)
        {
            graphics.DrawTitle();
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
            graphics.DrawFinish();

            //graphics.DrawText(System.Drawing.Color.White, 128, 128, "128 128!");

            //graphics.DrawText(System.Drawing.Color.White, 128, 374, "128 374!");

            //graphics.DrawText(System.Drawing.Color.White, 256, 256, "256 256!");

            //graphics.DrawText(System.Drawing.Color.White, 10, 10, "10 10!");
        }

        public GraphicsFacade.BoardRegion2D[] getRegions()
        {
            GraphicsFacade.BoardRegion2D[] ret = new GraphicsFacade.BoardRegion2D[]
            {
                new GraphicsFacade.BoardRegion2D(1, 224, 400, 64, 64),
                
                new GraphicsFacade.BoardRegion2D(2, 130,260,125,60),
                new GraphicsFacade.BoardRegion2D(3, 256, 260,125,60),
                new GraphicsFacade.BoardRegion2D(4, 130, 320, 125, 60),
                new GraphicsFacade.BoardRegion2D(5, 256, 320, 125, 60),
            };

            return ret;
        }
       
    }
}
