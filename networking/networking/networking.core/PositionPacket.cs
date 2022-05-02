using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoreNetworking
{
    public class PositionPacket : BasePacket
    {
        public Vector3 Position { get; private set; }

        public PositionPacket() : base(PacketType.Position)
        {
            Position = Vector3.zero;
        }

        public PositionPacket(Vector3 position, Player player) : base(PacketType.Position)
        {
            Position = position;
        }

        public byte[] Serialize()
        {
            base.StartSerialization();

            bw.Write(Position.x);
            bw.Write(Position.y);
            bw.Write(Position.z);

            return ms.GetBuffer();
        }

        public new void Deserialize(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }
    }
}