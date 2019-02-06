/****************************************************
    文件：LoginWnd.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/1/9 9:44:45
	功能：登陆界面控制
*****************************************************/

using Protocol;
using UnityEngine;
using UnityEngine.UI;

public class LoginWnd : WindowRoot
{
    [SerializeField] private InputField account;
    [SerializeField] private InputField password;
    [SerializeField] private Button bntEnter;
    [SerializeField] private Button bntPop;

    public string Acct { get; private set; }
    public string Pass { get; private set; }

    protected override void InitWnd()
    {
        base.InitWnd();
        ButtonAddListener();
        if (PlayerPrefs.HasKey("Account") && PlayerPrefs.HasKey("Password"))
        {
            SetText(account, PlayerPrefs.GetString("Account"));
            SetText(password, PlayerPrefs.GetString("Password"));
        }
    }

    private void ButtonAddListener()
    {
        bntEnter.onClick.AddListener(() =>
        {
            audio.PlayUIAudio(Constants.LoginBtn);
            Acct = account.text;
            Pass = password.text;
            if (Acct != "" && Pass != "")
            {
                NetMsg msg = new NetMsg
                {
                    cmd = (int)CMD.ReqLogin,
                    reqLogin = new ReqLogin() { account = Acct, password = Pass }
                };

                net.SendMsg(msg);
            }
            else
            {
                GameRoot.Instance.DynamicWnd.AddTips("账号或密码为空");
            }
        });

        bntPop.onClick.AddListener(() =>
        {
            audio.PlayUIAudio(Constants.ClickBtn);
            GameRoot.Instance.DynamicWnd.AddTips("该模块正在开发...");
        });
    }
}