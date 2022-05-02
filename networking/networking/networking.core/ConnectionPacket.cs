using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreNetworking
{
    public class ConnectionPacket : BasePacket
    {
        public ConnectionPacket() : base(PacketType.Connection)
        {
        }



    }
}
