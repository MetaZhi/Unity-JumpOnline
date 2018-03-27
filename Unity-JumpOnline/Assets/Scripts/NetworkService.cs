using LeanCloud.Realtime;
using UnityEngine;

public class NetworkService : MonoBehaviour
{
    public string AppId = "8RjJXnShOC4sa0tz9uyEuijJ-gzGzoHsz";
    public string AppKey = "k7p5fY1JRNGc8Toj7G6VF3Ee";
    public string ConversationId = "5aaf67f05b90c830ff7ce22a";

    AVRealtime _avRealtime;
    AVIMClient _client;
    AVIMConversation _lobbyConversation;

    private static NetworkService _instance;
    public static NetworkService Instance
    {
        get { return _instance; }
    }

    RoomManager _roomManager = new RoomManager();

    // Use this for initialization
    void Start()
    {
        _instance = this;
        // 使用 AppId 和 App Key 初始化 SDK
        _avRealtime = new AVRealtime(AppId, AppKey);
        // 方便调试将 WebSocket 日志打印在 Debug.Log 控制台上
        AVRealtime.WebSocketLog(Debug.LogWarning);
    }

    void OnDestroy()
    {
        _client.CloseAsync();
        _instance = null;
    }

    public void JoinLobby()
    {
        _avRealtime.CreateClientAsync(UserInfo.User).ContinueWith(t => _client = t.Result).ContinueWith(s =>
        {
            _client.OnMessageReceived += OnMessageReceived;
            Debug.Log("Joining");
            // 构建对话的时候需要指定一个 AVIMClient 实例做关联
            _lobbyConversation = AVIMConversation.CreateWithoutData(ConversationId, _client);
            _client.JoinAsync(_lobbyConversation).ContinueWith(a =>
            {
                if (a.IsFaulted)
                {
                    _lobbyConversation = null;
                    Debug.Log("Join failed");

                    return;
                }
                Debug.Log("Joined and sending message");
            });
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
            ProtocolManaer.Instance.Parse(textMessage.TextContent);
        }
    }

    public void Broadcast(AVIMMessage message)
    {
        _client.SendMessageAsync(_lobbyConversation, message);
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
        _avRealtime.CreateClientAsync("9999").ContinueWith(t => { zhouyu = t.Result; }).ContinueWith(s =>
        {
            // 关键参数是 isTransient: true
            zhouyu.CreateConversationAsync(
                name: "游戏大厅",
                isTransient: true).ContinueWith(t => { Debug.Log(t.Result.ConversationId); });
        });
    }
}