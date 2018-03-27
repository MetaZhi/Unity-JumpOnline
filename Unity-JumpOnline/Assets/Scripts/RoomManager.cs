using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public string HostId;
    public string Name;
}

public class RoomManager
{
    Dictionary<string, string> RoomMap = new Dictionary<string, string>();

    public void AddOrUpdate(string id, string name)
    {
        RoomMap[id] = name;
    }

    public void Remove(string id)
    {
        if (RoomMap.ContainsKey(id))
        {
            RoomMap.Remove(id);
        }
    }
}