using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    enum GameEventType
    {
        MovePiece,
        SelectPiece,
        RegionClick
    }
    abstract class GameEvent
    {
        public GameEventType type;
        public GameEvent(GameEventType type)
        {
            this.type = type;
        }
        public abstract int[] packMe();
    }
}
