using System.Collections;
using System.Collections.Generic;
using LeanCloud.Realtime;
using UnityEngine;

public class CreateRoom : Protocol
{
    public CreateRoom() : base("CreateRoom")
    {
    }

    public string Name;
}

public class RoomHostPanel : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}