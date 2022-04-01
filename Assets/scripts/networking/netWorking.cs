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

    void Start()
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        print("Connecting to server");
        socket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9000));
        socket.Blocking = false;

        isConnected = true;
        print("Connected to server yay");
        // fetch name then assign to player

        player = new Player(Guid.NewGuid().ToString("N"), GetPlayerName.pname);
        //instantiate player character
        InstantiateOverNetwork(GetPlayerName.CharacterType, Vector3.zero, Quaternion.identity);

    }

    public string GetPlayerID()
    {
        return player.ID;
    }

    public void Update()
    {
        if (isConnected)
        {
            if (socket.Available > 0)
            {
                byte[] buffer = new byte[socket.Available];
                socket.Receive(buffer);

                BasePacket bp = new BasePacket();
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
                    case BasePacket.PacketType.Instatite:
                        {
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
                    default:
                        break;
                }
            }
        }
    }

    public GameObject InstantiateOverNetwork(string prefabName, Vector3 position, Quaternion rotation)
    {
        GameObject go = Instantiate(Resources.Load($"Prefab/{prefabName}"), position, rotation) as GameObject;
        NetworkComponent nc = go.AddComponent<NetworkComponent>();
        nc.OwnerID = player.ID;
        nc.GameObjectID = Guid.NewGuid().ToString("N");
        socket.Send(new InstantiatePacket(prefabName, position, rotation, nc.GameObjectID, player).Serialize());

        return go;
    }

    GameObject InstantiateFromResources(string prefabName, Vector3 position, Quaternion rotation, string gameObjectID, Player player)
    {
        GameObject go = Instantiate(Resources.Load($"Prefab/{prefabName}"), position, rotation) as GameObject;
        NetworkComponent nc = go.AddComponent<NetworkComponent>();
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
}