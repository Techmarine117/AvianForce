using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreNetworking;
using System.Net;
using System.Net.Sockets;

namespace server
{
    internal class Lobby
    {
        List<Player> players = new List<Player>();
        

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
            for(int i = players.Count - 1; i >= 0; i--)
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
            if (players[i].ID == id)
                return players[i];

            }
            return null;
        }

        public void Update(Rooms rooms)
        {
            
            for(int j = players.Count - 1; j >= 0; j--)
            {
                //Console.WriteLine("Player loop");

                if(players[j].Socket.Available > 0)
                {
                   
                    //check if players have data,if so then Receive the data and deserialize the data
                    try
                    {
                        Socket client = players[j].Socket;
                        byte[] buffer = new byte[client.Available];
                        client.Receive(buffer);

                        Console.WriteLine("Message received!");

                        BasePacket bp = new BasePacket(client);
                        bp.Deserialize(buffer);

                        Console.WriteLine("Message deserialized");

                        Console.WriteLine(bp.Type);

                        switch (bp.Type)
                        {
                            // player will send either host or join packet. If host packet,deserialize the host packet to get player id and room id. Player is then added to a room.
                            case BasePacket.PacketType.HostGamePacket:
                                HostGamePacket hp = new HostGamePacket();
                                hp.Deserialize(buffer);
                                //makes a new room
                                rooms.AddRoom(players[j], hp.RoomID);
                                players.RemoveAt(j);

                                Console.WriteLine(hp.Player.Name + "Created new room with password:" + hp.RoomID);
                                break;
                            // if player wants to join, then deserialize the packet, try to get room ID
                            case BasePacket.PacketType.JoinPacket:  
                                JoinPacket jp = new JoinPacket();
                                jp.Deserialize(buffer);
                                
                                Room room = rooms.GetRoom(jp.RoomID);
                                
                                //if room id does not match, then try for next room. If room id match, then add player to the room and remove player from lobby
                                if (room != null)
                                {
                                    Console.WriteLine(jp.Player.Name + "Joined the room");
                                    room.AddPlayer(players[j]);
                                    players.RemoveAt(j);
                                }
                                else { 
                                    Console.WriteLine("Room null bro");
                                }

                                break;
                        }
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine(ex);
                    
                    
                    }
                        

                }
            }
        }
    }
}
