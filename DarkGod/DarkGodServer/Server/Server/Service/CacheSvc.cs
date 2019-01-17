/****************************************************
    文件：CacheSvc.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 16:32
	功能：缓存服务
*****************************************************/

using System.Collections.Generic;
using Protocol;

class CacheSvc : Singleton<CacheSvc>
{
    private Dictionary<string, ServerSession> onlineAccountDic = new Dictionary<string, ServerSession>();
    private Dictionary<ServerSession, PlayerData> onlineSessionDic = new Dictionary<ServerSession, PlayerData>();

    public void Init()
    {
        PECommon.Log("CachSvc Init..");
    }

    public bool IsAccountOnline(string account)
    {
        return onlineAccountDic.ContainsKey(account);
    }

    public PlayerData GetPlayerData(string account,string password)
    {
        //TODO 从数据库取数据
        return null;
    }

    public void AccountOnline(string account,ServerSession session,PlayerData playerData)
    {
        onlineAccountDic.Add(account, session);
        onlineSessionDic.Add(session, playerData);
    }
}

