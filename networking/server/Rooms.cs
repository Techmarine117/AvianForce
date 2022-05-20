using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreNetworking;

namespace server
{
    internal class Rooms
    {
        List<Room> rooms = new List<Room>();
        //adds player and room id to a room, has list with owner player and all players in room
        public void  AddRoom(Player Owner, string id)
        {
            rooms.Add(new Room(Owner, id));

        }

        public void RemoveRoomAtIndex(int index)
        {
            rooms.RemoveAt(index);
        }

        public void RemoveRoom(string id)
        {
            for(int i = rooms.Count - 1; i >= 0; i--)
            {
                if(rooms[i].ID == id)
                {
                    rooms.RemoveAt(i);
                    break;
                }
            }
        }

        public Room GetRoomAtIndex(int index)
        {
            //
            return rooms[index];
        }

        public Room GetRoom(string id)
        {
            //loops through all rooms to find room with matching ID
            for(int i = rooms.Count - 1; i >= 0; i--)
            {
                if(rooms[i].ID == id)
                    return rooms[i];
            }
            return null;

        }


    }
}
