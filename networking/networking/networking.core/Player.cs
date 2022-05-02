using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace CoreNetworking
{
    public class Player
    {
        public Socket Socket { get; private set; }
        public int ID { get; private set; }
        public string Name { get; private set; }

        public bool IsHost;

        public Player()
        {
            Socket = null;
            ID = 0;
            Name = "";
            IsHost = false;
            
        }

        public Player(Socket socket, int id, string name)
        {
            Socket = socket;
            ID = id;
            Name = name;
            IsHost = false;
        }



    }
}