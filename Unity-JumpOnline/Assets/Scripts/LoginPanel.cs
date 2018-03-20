using System.Collections;
using System.Collections.Generic;
using LeanCloud;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public InputField UserName;
    public InputField Password;
    public Text ErrorText;

    public GameObject LobbyPanel;

    // Use this for initialization
    void Start()
    {
        MainThreadDispatcher.Initialize();
    }

    public void Login()
    {
        var userName = UserName.text;
        var pwd = Password.text;
        AVUser.LogInAsync(userName, pwd).ContinueWith(t =>
        {
            MainThreadDispatcher.Post(_ =>
            {
                if (t.IsFaulted || t.IsCanceled)
                {
                    var error = t.Exception.Message; // 登录失败，可以查看错误信息。
                    Debug.Log(error);
                    ErrorText.text = error;
                }
                else
                {
                    //登录成功
                    Debug.Log("登陆成功");
                    UserInfo.User = t.Result;

                    // 加入realtime
                    RealTime.Instance.JoinLobby();

                    // 跳转到大厅界面
                    gameObject.SetActive(false);
                    LobbyPanel.SetActive(true);
                }
            }, null);
        });
    }
}