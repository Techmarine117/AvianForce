using CoreNetworking;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
//using UnityEngine;

namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
          
            socket.Blocking = false;
            socket.Bind(new IPEndPoint(IPAddress.Any, 9000));

            Console.WriteLine("waiting for a response");
            socket.Listen();

            List<Socket> clients = new List<Socket>();
            Lobby lobby = new Lobby();
            Rooms rooms = new Rooms();

            while (true)
            {
                // Connect to server
                try
                {
                    clients.Add(socket.Accept());
                    Console.WriteLine("response received");

                }
                catch (SocketException ex)
                {
                    if (ex.SocketErrorCode != SocketError.WouldBlock)
                        Console.WriteLine(ex);
                }
               

                for (int i = clients.Count - 1; i >= 0; i--)
                {
                    // Connect to lobby
                    if (clients[i].Available > 0)
                    {
                        try
                        {
                            Socket client = clients[i];
                            byte[] recieveBuffer = new byte[clients[i].Available];
                            clients[i].Receive(recieveBuffer);
                            BasePacket bp = new BasePacket(client);
                            bp.Deserialize(recieveBuffer);

                            Console.WriteLine("mESSAGE RECEIVED");
                            Console.WriteLine(bp.Type);

                            if(bp.Type == BasePacket.PacketType.Connection)
                            {
                                Console.WriteLine("Connection accepted");
                                lobby.AddPlayer(bp.Player);
                                clients.RemoveAt(i);
                            }
                        }
                        catch (SocketException ex)
                        {
                            if (ex.SocketErrorCode != SocketError.WouldBlock)
                            {
                                if (ex.SocketErrorCode == SocketError.ConnectionAborted || ex.SocketErrorCode == SocketError.ConnectionReset)
                                {
                                    clients[i].Close();
                                    clients.RemoveAt(i);
                                    Console.WriteLine($"Client{i} disconnected");
                                }
                                else
                                {
                                    Console.WriteLine(ex);
                                }
                            }
                        }
                    }
                }

            lobby.Update(rooms);
            }

        }
    }
}