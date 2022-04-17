using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreNetworking
{
    public class Player
    {
        public string ID { get; private set; }
        public string Name { get; private set; }

        public Player(string id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}