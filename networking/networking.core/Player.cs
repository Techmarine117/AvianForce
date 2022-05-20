
using System.Net.Sockets;

namespace CoreNetworking
{ //class with basic player information
    [System.Serializable]
    public class Player
    {
        public Socket Socket { get; private set; }
        public string ID { get; private set; }
        public string Name { get; private set; }

        public bool IsHost;

        public Player()
        {
            Socket = null;
            ID = "";
            Name = "";
            IsHost = false;

        }

        public Player(Socket socket, string id, string name)
        {
            Socket = socket;
            ID = id;
            Name = name;
            IsHost = false;
        }



    }
}