[System.Serializable]
public class MoveAction : Message {
    public string direction;

    public MoveAction(string direction)
    {
        this.type = "move";
        this.direction = direction;
    }
}
