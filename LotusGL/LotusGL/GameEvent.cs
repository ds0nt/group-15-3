using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL
{
    enum EventType
    {
        MovePiece
    }
    class GameEvent
    {
        public EventType type;
        public GameEvent(EventType type)
        {
            this.type = type;
        }
    }
}
