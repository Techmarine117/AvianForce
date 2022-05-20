using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreNetworking;

namespace server
{ //contains helper functions similar to Rooms.CS. Room has list of all players and who is the owner of the room( , Had ID of the room. Rooms is the players who are joining.
    internal class Room
    {
        List<Player> players = new List<Player>();

        public Player Owner { get; private set; }

        public string ID { get; private set; }

        public bool IsPlaying { get; private set; }

        public Room(Player master, string id)
        {
            players = new List<Player>();
            Owner = master;
            ID = id;
            IsPlaying = false;
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);

        }

        public void RemovePlayerAtIndex(int index)
        {
            players.RemoveAt(index);
        }

        public void RemovePlayer(string id)
        {
            for( int i = players.Count - 1; i >= 0; i--)
            {
                if(players[i].ID == id)
                {
                    players.RemoveAt(i);
                    break;
                }
            }
        }

        public Player GetPlayerAtIndex(int index)
        {
            return players[index];
        }

        public Player GetPlayer(string id)
        {
            for(int i = players.Count - 1; i >= 0; i--)
            {
                if(players[i].ID == id)
                    return players[i];
            }

            return null;
        }

    }
}
