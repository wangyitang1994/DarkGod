/****************************************************
    文件：NetSvc.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/1/12 10:55:20
	功能：网络服务
*****************************************************/

using PENet;
using Protocol;
using System.Collections.Generic;
using UnityEngine;

public class NetSvc : MonoBehaviour
{
    private static readonly string obj = "lock";
    public static NetSvc Instance;
    private PESocket<ClientSession, NetMsg> client;
    private Queue<NetMsg> msgQueue = null;
    public void InitNetSvc()
    {
        Instance = this;
        msgQueue = new Queue<NetMsg>();
        client = new PESocket<ClientSession, NetMsg>();
        client.StartAsClient(SerCfg.sevIP, SerCfg.sevPort);
        client.SetLog(true, (string msg, int lv) =>
        {
            switch (lv)
            {
                case 0:
                    Debug.Log("Log:" + msg);
                    break;
                case 1:
                    Debug.LogWarning("Warning:" + msg);
                    break;
                case 2:
                    Debug.LogError("Error:" + msg);
                    break;
                case 3:
                    Debug.Log("Info:" + msg);
                    break;
            }
        });
    }
    //向服务器发送消息
    public void SendMsg(NetMsg msg)
    {
        if (client.session == null)
        {
            GameRoot.Instance.DynamicWnd.AddTips("重新连接服务器...");
            InitNetSvc();
        }
        else
        {
            client.session.SendMsg(msg);
        }
    }

    //添加待执行消息
    private void AddMsgQue(NetMsg msg)
    {
        msgQueue.Enqueue(msg);
    }
    // 处理消息
    private void Update()
    {
        if (msgQueue.Count > 0)
        {
            lock (obj)
            {
                NetMsg msg = msgQueue.Dequeue();
                ProcessMsg(msg);
            }

        }
    }

    private void ProcessMsg(NetMsg msg)
    {
        if (msg.err == (int)Error.None)
        {
            switch (msg.cmd)
            {
                case (int)CMD.RspLogin:
                    LoginSys.Instance.RspLogin(msg);
                    break;
            }
        }
        else
        {
            switch (msg.err)
            {
                case (int)Error.AccountIsOnline:
                    GameRoot.Instance.DynamicWnd.AddTips("该账号已登陆...");
                    break;
                case (int)Error.WrongPass:
                    GameRoot.Instance.DynamicWnd.AddTips("账号密码错误...");
                    break;
            }
        }

    }
}