/****************************************************
    文件：ServerSession.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 10:47
	功能：服务器会话管理
*****************************************************/
using PENet;
using Protocol;

public class ServerSession : PESession<NetMsg>
{
    protected override void OnConnected()
    {
        PECommon.Log("Client Connected...");
        SendMsg(new NetMsg() { text = "Welcome to connected.." });
    }

    protected override void OnReciveMsg(NetMsg msg)
    {
        PECommon.Log("Recive:" + msg.cmd);
        PECommon.Log("Recive:" + msg.reqLogin.account + msg.reqLogin.password);
    }


    protected override void OnDisConnected()
    {
        PECommon.Log("Client Disconnected...");
    }
}

