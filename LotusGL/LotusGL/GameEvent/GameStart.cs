using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class GameStart : GameEvent
    {
        public Player[] players; // best variable ever
        public GameStart(Player[] players)
            : base(GameEventType.GameStart)
        {
            this.players = players;
        }

        public override byte[] packMe()
        {
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.MemoryStream());

            for (int i = 0; i < 4; i++)
            {
                writer.Write(players[i].color.ToArgb());
                writer.Write(players[i].name);
            }
            
            return ((System.IO.MemoryStream)writer.BaseStream).GetBuffer();
        }
        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            Player[] players = new Player[4];
            for (int i = 0; i < 4; i++)
            {
                players[i] = new Player(System.Drawing.Color.FromArgb(reader.ReadInt32()), 0, reader.ReadString());
            }
            
            return new GameStart(players);
        }
    }
}
