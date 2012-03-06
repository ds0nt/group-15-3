using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LotusGL.GameEvent
{
    class UpdateLobby : GameEvent
    {
        public Menu.LobbyData lobby;

        public UpdateLobby(Menu.LobbyData lobbydata)
            : base(GameEventType.UpdateLobby)
        {
            lobby = lobbydata;
        }

        public override byte[] packMe()
        {
            System.IO.BinaryWriter writer = new System.IO.BinaryWriter(new System.IO.MemoryStream());
            writer.Write(lobby.ptype[0]);
            writer.Write(lobby.ptype[1]);
            writer.Write(lobby.ptype[2]);
            writer.Write(lobby.ptype[3]);
            writer.Write(lobby.pname[0]);
            writer.Write(lobby.pname[1]);
            writer.Write(lobby.pname[2]);
            writer.Write(lobby.pname[3]);
            return ((System.IO.MemoryStream)writer.BaseStream).GetBuffer();
        }

        public static GameEvent Unpack(System.IO.BinaryReader reader)
        {
            Menu.LobbyData data = new Menu.LobbyData();
            data.ptype = new int[]
            {
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32(),
                reader.ReadInt32()
            };
            data.pname = new string[]
            {
                reader.ReadString(),
                reader.ReadString(),
                reader.ReadString(),
                reader.ReadString()
            };
            return new UpdateLobby(data);
        }
    }
}