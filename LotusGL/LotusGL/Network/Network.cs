using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LotusGL.Network
{
    interface Network
    {
        void Send(GameEvent.GameEvent willSend);
        GameEvent.GameEvent Receive();        
    }
}
