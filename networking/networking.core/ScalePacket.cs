using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoreNetworking
{
    public class ScalePacket : BasePacket
    {
        public Vector3 Scale { get; private set; }

        public ScalePacket() : base(PacketType.Scale, null)
        {
            Scale = Vector3.zero;
        }

        public ScalePacket(Vector3 scale, Player player) : base(PacketType.Scale, player)
        {
            Scale = scale;
        }

        public byte[] Serialize()
        {
            base.StartSerialization();

            bw.Write(Scale.x);
            bw.Write(Scale.y);
            bw.Write(Scale.z);

            return ms.GetBuffer();
        }

        public new void Deserialize(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            Scale = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }
    }
}