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
    public int sessionID;
    protected override void OnConnected()
    {
        sessionID = ServerRoot.Instance.GetSessionID();
        PECommon.Log("Client Connected...");
        SendMsg(new NetMsg() { text = "Welcome to connected.." });
    }

    protected override void OnReciveMsg(NetMsg msg)
    {
        MsgPack pack = new MsgPack()
        {
            serSession = this,
            netMsg = msg
        };
        NetSvc.Instance.AddMsgQue(pack);
        //PECommon.Log("Recive:" + msg.cmd);
        //PECommon.Log("Recive:" + msg.reqLogin.account + msg.reqLogin.password);
    }


    protected override void OnDisConnected()
    {
        LoginSys.Instance.CleanOffline(this);
        PECommon.Log("Client Disconnected...");
    }
}

