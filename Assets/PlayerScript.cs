using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public string playerId = null;
    NetworkScript network;

	// Use this for initialization
	void Start () {
        network = GameObject.Find("Network").GetComponent<NetworkScript>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerId != null) {
            if (Input.GetButtonUp("MoveUp"))
            {
                network.SendData(new MoveAction("n"));
            }
            if (Input.GetButtonUp("MoveDown"))
            {
                network.SendData(new MoveAction("s"));
            }
            if (Input.GetButtonUp("MoveLeft"))
            {
                network.SendData(new MoveAction("w"));
            }
            if (Input.GetButtonUp("MoveRight"))
            {
                network.SendData(new MoveAction("e"));
            }
        }
        
	}
}
