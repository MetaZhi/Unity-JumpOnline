using System.Collections;
using System.Collections.Generic;
using LeanCloud.Realtime;
using UnityEngine;

public class LobbyPanel : MonoBehaviour
{
    public GameObject RoomHostPanel;
    public GameObject RoomPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateRoom()
    {
        gameObject.SetActive(false);
        RoomHostPanel.SetActive(true);

        var protocol = new CreateRoom()
        {
            Name = UserInfo.User.Username + "的房间"
        };
        var message = new AVIMTextMessage(JsonUtility.ToJson(protocol));
        NetworkService.Instance.Broadcast(message);
    }
}
