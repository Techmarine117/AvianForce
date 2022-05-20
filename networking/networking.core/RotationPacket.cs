using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoreNetworking
{//gets rotation of object
    public class RotationPacket : BasePacket
    {
        public Quaternion Rotation { get; private set; }

        public RotationPacket() : base(PacketType.Rotation)
        {
            Rotation = Quaternion.identity;
        }

        public RotationPacket(Quaternion rotation, Player player) : base(PacketType.Rotation)
        {
            Rotation = rotation;
        }

        public byte[] Serialize()
        {
            base.StartSerialization();

            bw.Write(Rotation.x);
            bw.Write(Rotation.y);
            bw.Write(Rotation.z);
            bw.Write(Rotation.w);

            return ms.GetBuffer();
        }

        public new void Deserialize(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            Rotation = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }
    }
} 