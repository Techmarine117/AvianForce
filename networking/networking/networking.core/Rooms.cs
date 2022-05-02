using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreNetworking;
using System.Net;
using System.Net.Sockets;

namespace CoreNetworking
{
    public class Rooms
    {
        List<Socket> RoomClients = new List<Socket>();

        public string GameName { get; private set; }
        public string ID { get; private set; }



    }
}
