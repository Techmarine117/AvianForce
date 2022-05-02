using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoreNetworking
{
    public class DestroyPacket : BasePacket
    {
        public string GameObjectID { get; private set; }

        public DestroyPacket() : base(PacketType.Destroy)
        {
            GameObjectID = "";
        }

        public DestroyPacket(string gameObjectID, Player player) : base(PacketType.Destroy)
        {
            GameObjectID = gameObjectID;
        }

        public byte[] Serialize()
        {
            base.StartSerialization();

            bw.Write(GameObjectID);

            return ms.GetBuffer();
        }

        public new void Deserialize(byte[] buffer)
        {
            base.StartDeserialization(buffer);
            GameObjectID = br.ReadString();
        }
    }
}