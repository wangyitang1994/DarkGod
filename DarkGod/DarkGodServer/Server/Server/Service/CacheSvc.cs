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
    DataBaseMgr dbMgr = null;

    public void Init()
    {
        PECommon.Log("CachSvc Init..");
        dbMgr = DataBaseMgr.Instance;
    }
    //判断是否在线
    public bool IsAccountOnline(string account)
    {
        return onlineAccountDic.ContainsKey(account);
    }
    //从数据库找到玩家数据
    public PlayerData GetPlayerData(string account, string password)
    {
        return dbMgr.QueryPlayerData(account, password);
    }
    //用户登陆时缓存用户数据
    public void AccountOnline(string account, ServerSession session, PlayerData playerData)
    {
        onlineAccountDic.Add(account, session);
        onlineSessionDic.Add(session, playerData);
    }
    //注册时判断名称是否存在
    public bool IsNameExist(string name)
    {
        return dbMgr.QueryNameData(name);
    }
    //
    public PlayerData GetPlayerDataBySession(ServerSession session)
    {
        if (onlineSessionDic.TryGetValue(session, out PlayerData data))
        {
            return data;
        }
        return null;
    }
    //
    public bool UpdatePlayerData(int id, PlayerData data)
    {

        return dbMgr.UpdataPlayerData(id, data);
    }

}

