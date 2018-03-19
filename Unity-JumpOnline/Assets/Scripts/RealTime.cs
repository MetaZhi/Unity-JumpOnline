using System.Collections;
using System.Collections.Generic;
using LeanCloud.Realtime;
using UnityEngine;

public class RealTime : MonoBehaviour {
    AVRealtime avRealtime;
    // Use this for initialization
    void Start () {
        // 使用 AppId 和 App Key 初始化 SDK
        avRealtime = new AVRealtime("uay57kigwe0b6f5n0e1d4z4xhydsml3dor24bzwvzr57wdap", "kfgz7jjfsk55r5a8a3y4ttd3je1ko11bkibcikonk32oozww");
        // 方便调试将 WebSocket 日志打印在 Debug.Log 控制台上
        AVRealtime.WebSocketLog(Debug.Log);

        CreateChatRoom();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateChatRoom()
    {
        AVIMClient zhouyu = null;
        // 以周瑜的游戏 ID 3002 作为 client Id 构建 AVIMClient
        avRealtime.CreateClientAsync("3002").ContinueWith(t =>
        {
            zhouyu = t.Result;
        }).ContinueWith(s =>
        {
            // 关键参数是 isTransient: true
            zhouyu.CreateConversationAsync(member: "2002",
                name: "江东讨贼大联盟",
                isTransient: true,
                options: new Dictionary<string, object>()
                {
                {"topic","如何击败曹操的 80 万大军"}
                });
        });
    }
}
