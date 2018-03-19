using System.Collections;
using System.Collections.Generic;
using LeanCloud;
using UnityEngine;

public class LoginPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var userName = "demoUser";
        var pwd = "leancloud";
        AVUser.LogInAsync(userName, pwd).ContinueWith(t =>
        {
            if (t.IsFaulted || t.IsCanceled)
            {
                var error = t.Exception.Message; // 登录失败，可以查看错误信息。
                Debug.Log(error);
            }
            else
            {
                //登录成功
                Debug.Log("登陆成功");
            }
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
