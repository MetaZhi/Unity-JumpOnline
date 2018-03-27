using UnityEngine;

public class Protocol
{
    public string Id;
    public string Data;

    public Protocol(string id)
    {
        Id = id;
    }

    public void Deserialize(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }

    public string Serialize()
    {
        return JsonUtility.ToJson(this);
    }
}