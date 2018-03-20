using System.Collections;
using System.Collections.Generic;
using LeanCloud.Realtime;
using UnityEngine;

public class LobbyPanel : MonoBehaviour
{
    public GameObject CreateRoomPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateRoom()
    {
        gameObject.SetActive(false);
        CreateRoomPanel.SetActive(true);

        var protocol = new CreateRoom()
        {
            Name = UserInfo.User.Username + "的房间"
        };
        var message = new AVIMTextMessage(JsonUtility.ToJson(protocol));
        RealTime.Instance.Broadcast(message);
    }
}
