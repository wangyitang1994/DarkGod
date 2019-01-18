/****************************************************
    文件：NetSvc.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 10:48
	功能：网络服务管理
*****************************************************/


using System.Collections.Generic;
using PENet;
using Protocol;

public struct MsgPack
{
    public NetMsg netMsg;
    public ServerSession serSession;
    public MsgPack(ServerSession session, NetMsg msg)
    {
        netMsg = msg;
        serSession = session;
    }
}
public class NetSvc : Singleton<NetSvc>
{
    private Queue<MsgPack> msgPackQueue = new Queue<MsgPack>();
    private static readonly string obj = "lock";



    public void Init()
    {
        PECommon.Log("NetSvc Init..");
        PESocket<ServerSession, NetMsg> server = new PESocket<ServerSession, NetMsg>();
        server.StartAsServer(SerCfg.sevIP, SerCfg.sevPort);
    }
    //添加事件到队列
    public void AddMsgQue(MsgPack pack)
    {
        msgPackQueue.Enqueue(pack);
    }
    //执行（服务器是个多并发程序，需要上锁）
    public void Update()
    {
        if (msgPackQueue.Count > 0)
        {
            lock (obj)
            {
                MsgPack pack = msgPackQueue.Dequeue();
                HandOutMsg(pack);
            }
        }
    }
    //判断
    private void HandOutMsg(MsgPack pack)
    {
        switch ((CMD)pack.netMsg.cmd)
        {
            case CMD.ReqLogin:
                LoginSys.Instance.ReqLogin(pack);
                PECommon.Log("-->reqLogin");
                break;
            case CMD.ReqRename:
                LoginSys.Instance.ReqRename(pack);
                break;
        }
    }
}

