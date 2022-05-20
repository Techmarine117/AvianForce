using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoreNetworking
{ //sends room ID 
    public class JoinPacket : BasePacket
    {
        public string RoomID { get; private set; }

        public JoinPacket() : base(PacketType.JoinPacket)
        {
            Debug.Log("Hey I didn't get a room id");
        }

        public JoinPacket(string roomID) : base(PacketType.JoinPacket)
        {
            RoomID = roomID;
            Debug.Log("Let's win");
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
