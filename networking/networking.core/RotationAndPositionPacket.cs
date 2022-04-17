using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoreNetworking
{
    public class RotationAndPositionPacket : BasePacket
    {


        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }

        public RotationAndPositionPacket() : base(PacketType.RotationAndPosition, null)
        {
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
        }

        public RotationAndPositionPacket(Vector3 position, Player player , Quaternion rotation) : base(PacketType.RotationAndPosition, player)
        {
            Position = position;
            Rotation = rotation;
        }

        public byte[] Serialize()
        {
            base.StartSerialization();

            bw.Write(Position.x);
            bw.Write(Position.y);
            bw.Write(Position.z);


            bw.Write(Rotation.x);
            bw.Write(Rotation.y);
            bw.Write(Rotation.z);
            bw.Write(Rotation.w);


            return ms.GetBuffer();
        }

        public new void Deserialize(byte[] buffer)
        {
            base.StartDeserialization(buffer);

            Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            Rotation = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }
    }
}   

