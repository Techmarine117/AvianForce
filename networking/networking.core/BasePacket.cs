
using System.IO;
using System.Net.Sockets;


namespace CoreNetworking
{//template for all other packets, contains base traits for all packets
    public class BasePacket
    {
        protected MemoryStream ms;
        protected BinaryReader br;
        protected BinaryWriter bw;

        public enum PacketType
        {
            None = -1,

            Transform,
            Instantiate,
            Destroy,
            Position,
            Rotation,
            Scale,
            RotationAndPosition,
            HostGamePacket,
            JoinPacket,
            Connection
        }

        public PacketType Type { get; private set; }
        Socket socket;
        public Player Player { get;  set; }

        public BasePacket(Socket passedSocket)
        {
            this.socket = passedSocket;
            Type = PacketType.None;
        }

        public BasePacket(PacketType type)
        {
            Type = type;
            Player = new Player();
           
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
            Player = new Player(socket, br.ReadString(), br.ReadString());
        }

        public void Deserialize(byte[] buffer)
        {
            ms = new MemoryStream(buffer);
            br = new BinaryReader(ms);

            Type = (PacketType)br.ReadInt32();
            Player = new Player(socket,br.ReadString(), br.ReadString());
        }
    }
}