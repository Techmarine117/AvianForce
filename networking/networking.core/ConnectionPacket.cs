using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreNetworking
{ //sends the player's information when they first connect to the server
    public class ConnectionPacket : BasePacket
    {
        public ConnectionPacket() : base(PacketType.Connection)
        {



        }

            public byte[] Serialize()
            {
                base.StartSerialization();

                

                return ms.GetBuffer();
            }



    }
}
