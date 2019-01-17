/****************************************************
    文件：ClientSession.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/1/12 10:47:7
	功能：客户端会话管理
*****************************************************/

using PENet;
using Protocol;

public class ClientSession : PESession<NetMsg>
{
    protected override void OnConnected()
    {
        PECommon.Log("Server Connected..");
    }

    protected override void OnReciveMsg(NetMsg msg)
    {
        PECommon.Log("Recive:" + msg.text);
    }

    protected override void OnDisConnected()
    {
        PECommon.Log("Server Disconnected..");
    }
}