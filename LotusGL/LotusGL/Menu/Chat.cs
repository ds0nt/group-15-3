using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotusGL.Graphics;
namespace LotusGL.Menu
{
    class Chat : Menu
    {
        string[] chatlog;
        bool inputmode;
        int logspinner;
        string msg;
        
        public Chat()
        {
            chatlog = new string[4];
            logspinner = 0;
            inputmode = false;
        }

        public void handleRegionClick(int regionid)
        {

        }

        public void handleInput(char x)
        {
            if (x == char.MinValue)
                return;
            if (x == 1)
            {
                if (inputmode)
                {
                    chatlog[logspinner % chatlog.Length] = msg;
                    logspinner++;
                    msg = "";
                }

                inputmode = !inputmode;
            }
            else if (x == 2)
            {
                if(inputmode)
                    if(msg.Length >= 1)
                        msg = msg.Remove(msg.Length - 1);
            }
            else
            {
                if (inputmode)
                    msg = msg + x;
            }
        }

        public void Draw(Graphics.GraphicsFacade graphics)
        {
            float chatx = 128;
            float chaty = 30;
            float spacing = 14;

            if (inputmode)
            {
                graphics.DrawChatInput(128 - 16, 256 - 32, 256 + 32, 64);
                graphics.DrawText(System.Drawing.Color.White, 128, 256, msg);
            }
            graphics.DrawChatLog(chatx - 32, chaty - 20, 256 + 64, spacing * (chatlog.Length + 1));

            for (int i = logspinner; i < chatlog.Length + logspinner; i++)
            {
                graphics.DrawText(
                    System.Drawing.Color.White,
                    chatx,
                    chaty - ( - i + logspinner) * spacing,
                    chatlog[(i) % chatlog.Length]
                );
            }
        }

        public GraphicsFacade.BoardRegion2D[] getRegions()
        {
            GraphicsFacade.BoardRegion2D[] ret = new GraphicsFacade.BoardRegion2D[]
            {
                new GraphicsFacade.BoardRegion2D(1, -0.5f, 0f, 1f, 0.5f)
            };

            return ret;
        }
    }
}
