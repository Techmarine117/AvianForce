using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreNetworking
{//sends the room ID when a room is created
    public class HostGamePacket : BasePacket
    {
        public string RoomID { get; private set; }
      
        public HostGamePacket(): base(PacketType.HostGamePacket)
        {

        }

        public HostGamePacket(string roomID): base(PacketType.HostGamePacket)
        {
            RoomID = roomID;
        }

        public byte[] Serialize()
        {
            base.StartSerialization();

            bw.Write(RoomID);
            return ms.GetBuffer();
        }

        public new void Deserialize(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            RoomID = br.ReadString();

        }

    }
}
