using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL
{
    interface GameManager
    {
        void onGameEvent(GameEvent.GameEvent ge);
    }
}
