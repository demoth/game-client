[System.Serializable]
public class JoinMessage : Message {
    public string character_id;

    public JoinMessage(string character)
    {
        type = "join";
        character_id = character;
    }
}
