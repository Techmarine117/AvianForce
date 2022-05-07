using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Networking;
using System.Text;
using UnityEngine.Networking;
using System;
using UnityEngine.UI;


[Serializable]
public class PlayerData
{
    public string username;
    public string id;

    //input field

    public PlayerData(string username, string playerID)
    {
        this.username = username;
        this.id = playerID;
    }
}

public class Database : MonoBehaviour
{
    delegate void ReceivedJSON(string json);
    ReceivedJSON ReceivedJSONEvent;
    string mydata;
    InputField UsernameField;


    void Start()
    {
        //text input
        //netWorking script and GetPlayerID
        PlayerData playerData = new PlayerData(UsernameField.text,"12");
        string json = JsonUtility.ToJson(playerData);

        //StartCoroutine(HTTPCall("http://127.0.0.1:3000/save-user-data", "post", json));
        StartCoroutine(HTTPCall($"http://127.0.0.1:3000/find-user-data?username={UsernameField.text}&id=12", "get")); //these methods are here to test if the server works. 
        //this need to be put into appropriate methods that are called when needed and not just automatically whenever the server is started.
        print(mydata);

    }

    void OnReceivedJSON(string json)
    {
        PlayerData p = JsonUtility.FromJson<PlayerData>(json);

        print(p.username);
        print(p.id);
    }

    IEnumerator HTTPCall(string url, string method, string json = "")
    {
        using (UnityWebRequest request = new UnityWebRequest(url, method))
        {
            if (method == "post")
            {
                byte[] data = Encoding.UTF8.GetBytes(json);
                request.uploadHandler = new UploadHandlerRaw(data);
            }
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.error == null)
            {
                print("all good");
                print(request.downloadHandler.text);
            }
            else
            {
                print("error");
                print(request.error);
            }
        }
    }
}