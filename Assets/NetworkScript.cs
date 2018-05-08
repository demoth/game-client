using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class NetworkScript : MonoBehaviour {
	private WebSocket webSocket;
	private string serverAddress;
    private LoginScript loginScript;
    private GameObject loginPanel;
    private ObjectManagerScript objectManager;
    private PlayerScript playerScript;
    // Use this for initialization
    void Start () {
		loginScript = GameObject.Find("LoginPanel").GetComponent<LoginScript>();
        loginPanel = GameObject.Find("LoginPanel");
        objectManager = GameObject.Find("ObjectManager").GetComponent<ObjectManagerScript>();
        playerScript = GameObject.Find("PlayerManager").GetComponent<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SendData(Message msg){
        //string data = fastJSON.JSON.ToJSON(msg, p);
        string data = JsonUtility.ToJson(msg);
        Debug.Log("Sending: " + data);
        webSocket.SendString(data);
	}

	public void ConnectTo(string url) {
		serverAddress = url;
		StartCoroutine ("WebSocketCoroutine");
	}

	IEnumerator WebSocketCoroutine () {

		webSocket = new WebSocket(new Uri(serverAddress));
		yield return StartCoroutine(webSocket.Connect());

		while (true)
		{
			string reply = webSocket.RecvString();
            if (reply != null)
			{
                Debug.Log("Received msg:" + reply);
                Message msg = fastJSON.JSON.ToObject<Message>(reply);
                switch (msg.type)
                {
                    case "logged_in":
                        LoggedInMessage lm = fastJSON.JSON.ToObject<LoggedInMessage>(reply);
                        Debug.Log("Logged in!, characters: " + lm.characters);
                        loginScript.AddCharacters(lm.characters);
                        break;
                    case "joined":
                        JoinedMessage jm = fastJSON.JSON.ToObject<JoinedMessage>(reply);
                        playerScript.playerId = jm.id;
                        // todo: camera follow
                        Debug.Log("Joined!, playerId = " + jm.id);
                        loginPanel.SetActive(false);
                        break;
                    case "update":
                        UpdateMessage umsg = (UpdateMessage) fastJSON.JSON.ToObject(reply, typeof(UpdateMessage));
                        Debug.Log("update type: " + umsg.type);
                        Debug.Log("updates: " + umsg.updates);
                        Debug.Log("List size: " + umsg.updates.Count);
                        foreach (var m in umsg.updates)
                        {
                            Dictionary<string, object> update = (Dictionary<string, object>)m;
                            var type = (string)update["type"];
                            Debug.Log("update.type = " + type);
                            switch (type)
                            {
                                case "appear":
                                    Debug.Log("update.type = " + update["type"]);
                                    Debug.Log("update.object_type = " + update["object_type"]);
                                    Debug.Log("update.id = " + update["id"]);
                                    Debug.Log("update.x = " + update["x"]);
                                    Debug.Log("update.y = " + update["y"]);

                                    objectManager.InstantiateObject((string)update["object_type"], (string)update["id"], (long)update["x"], (long)update["y"]);
                                    break;
                                case "disappear":
                                    Debug.Log("Removing object");
                                    objectManager.DestroyObject((string)update["id"]);
                                    break;
                                case "change":
                                    Debug.Log("update.id = " + update["id"]);
                                    Debug.Log("update.field = " + update["field"]);
                                    Debug.Log("update.new_value = " + update["new_value"]);
                                    objectManager.Change((string)update["id"], (string)update["field"], (string)update["new_value"]);
                                    break;
                                case "movement":
                                    Debug.Log("update.id = " + update["id"]);
                                    Debug.Log("update.x = " + update["x"]);
                                    Debug.Log("update.y = " + update["y"]);
                                    objectManager.Move((string)update["id"], (long)update["x"], (long)update["y"]);
                                    break;

                            }
                        }
                        break;
                }
                Debug.Log("Received type: " + msg.type);
			}
			if (webSocket.error != null)
			{
				Debug.Log(reply);
				break;
			}
			yield return new WaitForSeconds(0.01f);
		}
	}

}
