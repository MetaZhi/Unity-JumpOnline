using System.Collections;
using System.Collections.Generic;
using LeanCloud.Realtime;
using UnityEngine;

public class RealTime : MonoBehaviour
{
    public string AppId = "8RjJXnShOC4sa0tz9uyEuijJ-gzGzoHsz";
    public string AppKey = "k7p5fY1JRNGc8Toj7G6VF3Ee";
    public string ConversationId = "5aaf67f05b90c830ff7ce22a";

    AVRealtime avRealtime;

    // Use this for initialization
    void Start()
    {
        // 使用 AppId 和 App Key 初始化 SDK
        avRealtime = new AVRealtime(AppId, AppKey);
        // 方便调试将 WebSocket 日志打印在 Debug.Log 控制台上
        AVRealtime.WebSocketLog(Debug.LogWarning);
        JoinLobby();
    }

    public void JoinLobby()
    {
        string clientId = Random.Range(1000, 10000).ToString();
        AVIMClient zhouyu = null;
        AVIMConversation lobbyConversation = null;
        // 以周瑜的游戏 ID 3002 作为 client Id 构建 AVIMClient
        avRealtime.CreateClientAsync(clientId).ContinueWith(t => zhouyu = t.Result).ContinueWith(s =>
        {
            zhouyu.OnMessageReceived += OnMessageReceived;
            Debug.Log("Joining");
            // 构建对话的时候需要指定一个 AVIMClient 实例做关联
            lobbyConversation = AVIMConversation.CreateWithoutData(ConversationId, zhouyu);
            // 直接邀请赵云加入对话
            return zhouyu.JoinAsync(lobbyConversation);
        }).ContinueWith(s =>
        {
            Debug.Log("Joined and sending message");

            zhouyu.SendMessageAsync(lobbyConversation, new AVIMTextMessage("hello, can you hear me?"));
        });
    }

    private void OnMessageReceived(object sender, AVIMMessageEventArgs e)
    {
        if (e.Message is AVIMTextMessage)
        {
            var textMessage = (AVIMTextMessage)e.Message;
            // textMessage.ConversationId 是该条消息所属于的对话 Id
            // textMessage.TextContent 是该文本消息的文本内容
            // textMessage.FromClientId 是消息发送者的 client Id
            Debug.Log(string.Format("你收到来自于 Id 为 {0} 的对话的文本消息，消息内容是： {1}，发送者的 client Id 是 {2}", textMessage.ConversationId, textMessage.TextContent, textMessage.FromClientId));
        }
    }

    [ContextMenu("Create Lobby")]
    void CreateLobby()
    {
        if (!Application.isPlaying)
        {
            Debug.LogError("Run this only in play mode");
            return;
        }
        AVIMClient zhouyu = null;
        avRealtime.CreateClientAsync("9999").ContinueWith(t => { zhouyu = t.Result; }).ContinueWith(s =>
        {
            // 关键参数是 isTransient: true
            zhouyu.CreateConversationAsync(
                name: "游戏大厅",
                isTransient: true).ContinueWith(t => { Debug.Log(t.Result.ConversationId); });
        });
    }
}