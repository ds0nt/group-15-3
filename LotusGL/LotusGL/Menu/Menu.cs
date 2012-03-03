using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.Menu
{
    interface Menu
    {
        LotusGL.Graphics.GraphicsFacade.BoardRegion2D[] getRegions();
        void Draw(Graphics.GraphicsFacade graphics);
        void handleRegionClick(int regionid);
    }
}
