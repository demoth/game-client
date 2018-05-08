[System.Serializable]
public class LoginMessage : Message {
	public string login;
	public string password;

    public LoginMessage(string user, string pass)
    {
        this.login = user;
        this.password = pass;
        this.type = "login";
    }
}
