using System.Collections.Generic;
using UnityEngine;

public class ProtocolManaer
{
    private static ProtocolManaer _instance;

    private ProtocolManaer()
    {
        
    }

    public static ProtocolManaer Instance
    {
        get { return _instance ?? (_instance = new ProtocolManaer()); }
    }

    Dictionary<string, Protocol> _protocolMap = new Dictionary<string, Protocol>();

    public void Regist(string id, Protocol proto)
    {
        _protocolMap[id] = proto;
    }

    public Protocol Parse(string json)
    {
        var proto = JsonUtility.FromJson<Protocol>(json);
        var instance = _protocolMap[proto.Id];

        instance.Deserialize(json);
        return instance;
    }
}