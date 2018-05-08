[System.Serializable]
public class AppearData : Message {

    public string object_type;
    public string id;
    public int x;
    public int y;

    public AppearData()
    {
        type = "appear";
    }

}
