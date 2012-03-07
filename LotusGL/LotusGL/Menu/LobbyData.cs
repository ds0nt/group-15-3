using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.Menu
{
    class LobbyData
    {
        //used for ai/none/etc
        public int[] ptype = {0,0,0,0};
        
        //Used for joiners
        public string[] pname = {"","","",""};
    
        public void pnext(int index)
        {
            if (pname[index] == "" || ptype[index] == 3)
            {
                ptype[index] = (ptype[index] + 1) % 4;
                if (ptype[index] == 3)
                {
                    pname[index] = LotusGame.get().name;
                }
                else
                {
                    pname[index] = "";
                }
            }
        }

        public Player[] createPlayers()
        {
            return new Player[]
            {
                new Player(System.Drawing.Color.Red, ptype[0], pname[0]),
                new Player(System.Drawing.Color.White, ptype[1], pname[1]),
                new Player(System.Drawing.Color.Gray, ptype[2], pname[2]),
                new Player(System.Drawing.Color.Blue, ptype[3], pname[3])
            };
        }

        public void AddName(string name)
        {
            for (int i = 0; i < 4; i++)
            {
                if (pname[i] == "")
                {
                    ptype[i] = 0;
                    pname[i] = name;
                    return;
                }
            }
        }

        float[] x = { 130, 256, 130, 256 };
        float[] y = { 260, 260, 320, 320 };
        float width = 125;
        float height = 60;

        public void Draw(Graphics.GraphicsFacade graphics)
        {
            if (Graphics.GraphicsFacade.mode != Graphics.GraphicsFacade.Mode.MENU)
                return;
            for (int i = 0; i < 4; i++)
            {
                switch (ptype[i])
                {
                    case 0:
                        switch (i)
                        {
                            case 0:
                                graphics.DrawPlayer1();
                                break;
                            case 1:
                                graphics.DrawPlayer2();
                                break;
                            case 2:
                                graphics.DrawPlayer3();
                                break;
                            case 3:
                                graphics.DrawPlayer4();
                                break;
                        }
                        break;
                    case 1:
                        graphics.DrawRuleAI(x[i], y[i], width, height);
                        break;
                    case 2:
                        graphics.DrawStateAI(x[i], y[i], width, height);
                        break;
                    case 3:
                        graphics.DrawHuman(x[i], y[i], width, height);
                        break;
                    
                }
                graphics.DrawText(System.Drawing.Color.White, x[i], y[i] + 60, pname[i]);
            }      
        }
    }
}
