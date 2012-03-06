using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LotusGL.Graphics;
namespace LotusGL.Menu
{
    class EnterIP : Menu
    {
        public bool inputmode;
        public string address;

        public EnterIP()
        {
            inputmode = false;
            address = "";
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
                if(inputmode)
                    inputmode = !inputmode;
            }
            else if (x == 2)
            {
                if (inputmode)
                    if (address.Length >= 1)
                        address = address.Remove(address.Length - 1);
            }
            else
            {
                if("1234567890.".Contains(x))
                    if (inputmode)
                        address = address + x;
            }
        }

        public void Draw(Graphics.GraphicsFacade graphics)
        {
            if (inputmode)
            {
                graphics.DrawChatInput(128 - 16, 256 - 32, 256 + 32, 64);
                graphics.DrawText(System.Drawing.Color.White, 128, 256, address);
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
