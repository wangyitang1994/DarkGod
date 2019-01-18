/****************************************************
    文件：CreateWnd.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/1/10 9:49:46
	功能：创建角色窗口管理
*****************************************************/

using Protocol;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CreateWnd : WindowRoot
{
    [SerializeField] private InputField playerName;
    [SerializeField] private Button bntRandom;
    [SerializeField] private Button bntEnter;

    protected override void InitWnd()
    {
        base.InitWnd();
        ButtonAddListener();
        ShowRandomName();
    }

    private void ButtonAddListener()
    {
        bntRandom.onClick.AddListener(() =>
        {
            ShowRandomName();
        });
        bntEnter.onClick.AddListener(() =>
        {
            if (playerName.text != "")
            {
                //TODO 进入游戏
                NetMsg msg = new NetMsg()
                {
                    cmd = (int)CMD.ReqRename,
                    reqRename = new ReqRename()
                    {
                        name = playerName.text
                    }
                };
                net.SendMsg(msg);
            }
            else
            {
                GameRoot.Instance.DynamicWnd.AddTips("用户名不能为空");
            }
        });
    }

    private void ShowRandomName()
    {
        SetText(playerName, res.GetRandomName(false));

    }
}