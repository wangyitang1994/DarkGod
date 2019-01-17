/****************************************************
    文件：LoginSys.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/09 16:12
	功能：登陆系统
*****************************************************/

using Protocol;
using UnityEngine;

public class LoginSys : SystemRoot
{
    public static LoginSys Instance;
    [SerializeField] private CreateWnd createWnd;
    public CreateWnd CreateWnd
    {
        get { return createWnd; }
    }
    [SerializeField] private LoginWnd loginWnd;
    public LoginWnd LoginWnd
    {
        get { return loginWnd; }
    }

    public override void InitSystem()
    {
        base.InitSystem();
        Instance = this;
    }

    public void EnterLogin()
    {
        resSvc.AsynLoadingScene(Constants.SceneLogin, () =>
        {
            loginWnd.SetWndState();
            audioSvc.PlayBGM(Constants.LoginBGM);
        });
    }

    public void RspLogin(NetMsg msg)
    {
        
        PECommon.Log("登陆成功");
        //TODO 根据msg显示
        createWnd.SetWndState();
        loginWnd.SetWndState(false);
    }

}
