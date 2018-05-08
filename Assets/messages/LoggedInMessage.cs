using System.Collections.Generic;

[System.Serializable]
public class LoggedInMessage : Message {
    public List<string> characters;

    public LoggedInMessage()
    {
        type = "logged_in";
    }

}
