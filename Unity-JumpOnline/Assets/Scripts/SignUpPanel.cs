using System.Collections;
using System.Collections.Generic;
using LeanCloud;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class SignUpPanel : MonoBehaviour
{
    public InputField UserName;
    public InputField Password;
    public InputField Email;
    public Text ErrorText;
    public Button SignUpButton;

	// Use this for initialization
	void Start () {
        MainThreadDispatcher.Initialize();
    }
	
	// Update is called once per frame
    public void SignUp()
    {
        var userName = UserName.text;
        var pwd = Password.text;
        var email = Email.text;
        var user = new AVUser();
        user.Username = userName;
        user.Password = pwd;
        user.Email = email;
        SignUpButton.interactable = false;
        user.SignUpAsync().ContinueWith(t =>
        {
            MainThreadDispatcher.Send(_ =>
            {
                SignUpButton.interactable = true;
                var uid = user.ObjectId;
                if (t.IsFaulted)
                {
                    Debug.Log(t.Exception.InnerExceptions[0].Message);
                    ErrorText.text = t.Exception.InnerExceptions[0].Message;
                    return;
                }
                Debug.Log(uid);
                UserInfo.User = user;

            }, null);
        });
    }
}
