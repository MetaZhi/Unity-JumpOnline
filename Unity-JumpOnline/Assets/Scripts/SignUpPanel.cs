using System.Collections;
using System.Collections.Generic;
using LeanCloud;
using UnityEngine;

public class SignUpPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
        var userName = "demoUser";
        var pwd = "leancloud";
        var email = "xxx@qq.com";
        var user = new AVUser();
        user.Username = userName;
        user.Password = pwd;
        user.Email = email;
        user.SignUpAsync().ContinueWith(t =>
        {
            var uid = user.ObjectId;
            Debug.Log(t.Exception.InnerExceptions[0].Message);
            Debug.Log(uid);
        });
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
