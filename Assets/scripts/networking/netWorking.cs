using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using CoreNetworking;
using System;


public class netWorking : MonoBehaviour
{
    Socket socket;
    Player player;
    bool isConnected = false;
    NetworkComponent nc;
    Database db;
    PlayerData playerData;

    void Start()
    {

        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        print("Connecting to server");
        socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000));
        socket.Blocking = false;

        isConnected = true;
        print("Connected to server yay");
        // fetch name then assign to player
        player = new Player(socket, "1", "we");
        //instantiate player character

        // Called when game scene loads.
        InstantiateOverNetwork(GetPlayerName.CharacterType, Vector3.zero, Quaternion.identity);
       



    }
   
    public string GetPlayerID()
    {
        return player.ID;
    }

    public void Connect()
    {
        print("Connected");
        ConnectionPacket connection = new ConnectionPacket();
        print(player == null);
        connection.Player = player;
        socket.Send(connection.Serialize());


    }

    public void ServerConnect()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); //synchronous
        print("Connecting to server");
        socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000));
        socket.Blocking = false;

        isConnected = true;
        print("Connected to server yay");

        //fetch player name from db
        // player = new Player(Guid.NewGuid().ToString("N"), GetPlayerName.pname);
    }


    public void Update()
    {
        if (isConnected)
        {
            if (socket.Available > 0)
            {
                byte[] buffer = new byte[socket.Available];
                socket.Receive(buffer);

                Debug.Log("Packet Received");

                BasePacket bp = new BasePacket(socket);
                bp.Deserialize(buffer);

                switch (bp.Type)
                {
                    case BasePacket.PacketType.None:
                        break;

                    case BasePacket.PacketType.Transform:
                        {
                            RotationAndPositionPacket rpp = new RotationAndPositionPacket();
                            rpp.Deserialize(buffer);


                            break;
                        }
                    case BasePacket.PacketType.Instantiate:
                        {
                            Debug.Log("Received instantiate packet");
                            InstantiatePacket ip = new InstantiatePacket();
                            ip.Deserialize(buffer);

                            print(ip.Player.ID);
                            print(ip.Player.Name);
                            print(ip.PrefabName);

                            InstantiateFromResources(ip.PrefabName, ip.Position, ip.Rotation, ip.GameObjectID, ip.Player);
                            break;
                        }

                    case BasePacket.PacketType.Destroy:
                        {
                            print("hi");
                            DestroyPacket dp = new DestroyPacket();
                            dp.Deserialize(buffer);
                            print(dp.GameObjectID);

                            DestroyObject(dp.GameObjectID);
                            break;
                        }

                    case BasePacket.PacketType.HostGamePacket:
                        {

                            HostGamePacket Hp = new HostGamePacket();
                            Hp.Deserialize(buffer);

                            HostGame(Hp.RoomID, Hp.Player);
                            break;
                        }

                    case BasePacket.PacketType.JoinPacket:
                        {
                            JoinPacket join = new JoinPacket();
                            join.Deserialize(buffer);

                            break;
                        }


                    case BasePacket.PacketType.Connection:
                        {
                            ConnectionPacket CP = new ConnectionPacket();
                            CP.Deserialize(buffer);


                           
                            break;
                        }



                    default:
                        break;
                }
            }
        }
    }

    public GameObject InstantiateOverNetwork(string prefabName, Vector3 position, Quaternion rotation)
    {
        GameObject go = Instantiate(Resources.Load($"Prefab/{prefabName}"), position, rotation) as GameObject;
        nc = go.AddComponent<NetworkComponent>();
        nc.OwnerID = player.ID;
        nc.GameObjectID = Guid.NewGuid().ToString("N");
        socket.Send(new InstantiatePacket(prefabName, position, rotation, nc.GameObjectID).Serialize());

        return go;
    }

    GameObject InstantiateFromResources(string prefabName, Vector3 position, Quaternion rotation, string gameObjectID, Player player)
    {
        GameObject go = Instantiate(Resources.Load($"Prefab/{prefabName}"), position, rotation) as GameObject;
        nc = go.AddComponent<NetworkComponent>();
        nc.OwnerID = player.ID;
        nc.GameObjectID = gameObjectID;


        return go;
    }

    void DestroyObject(string GameObjectID)
    {
        NetworkComponent[] ncs = FindObjectsOfType<NetworkComponent>();

        for (int i = 0; i < ncs.Length; i++)
        {
            if (ncs[i].GameObjectID == GameObjectID)
            {
                Destroy(ncs[i].gameObject);
                break;
            }
        }
    }

    void HostGame(string RoomId, Player player)
    {
        nc.GameId = RoomId;
        socket.Send(new HostGamePacket(RoomId).Serialize());


    }


   public void HostGame_tmp()
    {
        Debug.Log("Button clicked!");

        

        string RoomId = "0";
        nc.GameId = RoomId;
        socket.Send(new HostGamePacket(RoomId).Serialize());

        Debug.Log("Packet sent!");


    }


 public   void JoinLobby()
    { 
        string Roomid = "0";
       
        socket.Send(new JoinPacket(Roomid).Serialize());


    }

}