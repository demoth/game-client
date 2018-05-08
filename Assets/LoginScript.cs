using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour {
	InputField url;
	InputField usernameInput;
	InputField passwordInput;
    Dropdown charDropdown;
	NetworkScript network;

	// Use this for initialization
	void Start () {
		Debug.Log ("Init login script");
        network = GameObject.Find("Network").GetComponent<NetworkScript>();

        url = GameObject.Find("ServerInput").GetComponent<InputField>();
        usernameInput = GameObject.Find("UsernameInput").GetComponent<InputField>();
        passwordInput = GameObject.Find("PasswordInput").GetComponent<InputField>();


        Button b = GameObject.Find ("ServerConnectBtn").GetComponent<Button>();
		b.onClick.AddListener (Connect);

        Button l = GameObject.Find("LoginBtn").GetComponent<Button>();
        l.onClick.AddListener(Login);

        Button j = GameObject.Find("JoinBtn").GetComponent<Button>();
        j.onClick.AddListener(Join);

        charDropdown = GameObject.Find("CharDropDown").GetComponent<Dropdown>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Login()
    {
        LoginMessage m = new LoginMessage(usernameInput.text, passwordInput.text);
        network.SendData(m);
    }

    public void Connect() {
		Debug.Log ("connecting to " + url.text);
		network.ConnectTo (url.text);
	}

    public void AddCharacters(List<string> chars)
    {
        charDropdown.ClearOptions();
        charDropdown.AddOptions(chars);
    }

    public void Join()
    {
        string character = charDropdown.options[charDropdown.value].text;
        network.SendData(new JoinMessage(character));
    }
}
