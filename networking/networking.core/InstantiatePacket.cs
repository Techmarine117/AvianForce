using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CoreNetworking
{ //instantiate player object
    public class InstantiatePacket : BasePacket
    {
        public string GameObjectID { get; private set; }
        public string PrefabName { get; private set; }

        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }

        public InstantiatePacket() : base(PacketType.Instantiate)
        {
            GameObjectID = "";
            PrefabName = "";
            Position = Vector3.zero;
            Rotation = Quaternion.identity;
        }

        public InstantiatePacket(string prefabName, Vector3 position, Quaternion rotation, string gameObjectID) : base(PacketType.Instantiate)
        {
            GameObjectID = gameObjectID;
            PrefabName = prefabName;
            Position = position;
            Rotation = rotation;
        }

        public byte[] Serialize()
        {
            base.StartSerialization();

            bw.Write(GameObjectID);

            bw.Write(PrefabName);

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

            GameObjectID = br.ReadString();

            PrefabName = br.ReadString();

            Position = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
            Rotation = new Quaternion(br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        }
    }
}