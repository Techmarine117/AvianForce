using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CoreNetworking
{
    public class BasePacket
    {
        protected MemoryStream ms;
        protected BinaryReader br;
        protected BinaryWriter bw;

        public enum PacketType
        {
            None = -1,

            Transform,
            Instatite,
            Destroy,
            Position,
            Rotation,
            Scale,
            RotationAndPosition,
            HostGamePacket
        }

        public PacketType Type { get; private set; }
        public Player Player { get; private set; }

        public BasePacket()
        {
            Type = PacketType.None;
            Player = null;
        }

        public BasePacket(PacketType type, Player player)
        {
            Type = type;
            Player = player;
        }

        protected void StartSerialization()
        {
            ms = new MemoryStream();
            bw = new BinaryWriter(ms);

            bw.Write((int)Type);
            bw.Write(Player.ID);
            bw.Write(Player.Name);
        }

        protected void StartDeserialization(byte[] buffer)
        {
            ms = new MemoryStream(buffer);
            br = new BinaryReader(ms);

            Type = (PacketType)br.ReadInt32();
            Player = new Player(br.ReadString(), br.ReadString());
        }

        public void Deserialize(byte[] buffer)
        {
            ms = new MemoryStream(buffer);
            br = new BinaryReader(ms);

            Type = (PacketType)br.ReadInt32();
            Player = new Player(br.ReadString(), br.ReadString());
        }
    }
}