using CoreNetworking;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            List<Rooms> rooms = new List<Rooms>();

            socket.Bind(new IPEndPoint(IPAddress.Any, 9000));

            Console.WriteLine("waiting for a response");
            socket.Listen();

            List<Socket> clients = new List<Socket>();

            while (true)
            {
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
                for (int k = rooms.Count - 1; k >= 0; k--)
                {

                }

                for (int i = clients.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        byte[] recieveBuffer = new byte[clients[i].Available];
                        clients[i].Receive(recieveBuffer);

                        for (int j = clients.Count - 1; j >= 0; j--)
                        {
                            if (i == j)
                                continue;

                            clients[j].Send(recieveBuffer);
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
        }
    }
}