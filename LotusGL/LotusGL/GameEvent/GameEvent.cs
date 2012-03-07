using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace LotusGL.GameEvent
{
    enum GameEventType
    {
        MovePiece = 0,
        SelectPiece = 1,
        RegionClick = 2,
        GameOver = 3,
        AITurn = 4,
        SetPlayer = 5,
        GameStart = 6,
        SetName = 7,
        UpdateLobby = 8,
        ChatMessage = 9,
    }

    abstract class GameEvent
    {
        public GameEventType type;
        public GameEvent(GameEventType type)
        {
            this.type = type;
        }
        public static GameEvent parseBytes(byte[] data)
        {
            BinaryReader reader = new BinaryReader(new MemoryStream(data));
            int enumcode = reader.ReadInt32();
            Console.WriteLine("Enum: " + enumcode);
            switch (enumcode)
            {
                case (int) GameEventType.RegionClick:
                    return RegionClick.Unpack(reader);
                case (int) GameEventType.MovePiece:
                    return Move.Unpack(reader);
                case (int) GameEventType.GameOver:
                    return GameOver.Unpack(reader);
                case (int) GameEventType.SelectPiece:
                    return Select.Unpack(reader);
                case (int) GameEventType.SetPlayer:
                    return ChangePlayer.Unpack(reader);
                case (int)GameEventType.GameStart:
                    return GameStart.Unpack(reader);
                case (int)GameEventType.SetName:
                    return SetName.Unpack(reader);
                case (int)GameEventType.UpdateLobby:
                    return UpdateLobby.Unpack(reader);
                case (int)GameEventType.ChatMessage:
                    return ChatMessage.Unpack(reader);
            }
            return null;
        }
        public abstract byte[] packMe();
    }
}
