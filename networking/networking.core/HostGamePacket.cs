using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreNetworking
{
    public class HostGamePacket : BasePacket
    {
        public string GameName { get; private set; }
        public string ID { get; private set; }
        

       public HostGamePacket() : base(PacketType.HostGamePacket, null)
        {
            GameName = "";
            ID = "";

        }

        public HostGamePacket(string id,string name, Player player) : base(PacketType.HostGamePacket, player)
        {
            ID = id;
            GameName = name;
        }
    
      


    }
}
