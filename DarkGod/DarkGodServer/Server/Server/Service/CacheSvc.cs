/****************************************************
    文件：CacheSvc.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 16:32
	功能：缓存服务
*****************************************************/

using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
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
        if (!onlineAccountDic.ContainsKey(account)) onlineAccountDic.Add(account, session);
        if (!onlineSessionDic.ContainsKey(session)) onlineSessionDic.Add(session, playerData);
    }
    //注册时判断名称是否存在
    public bool IsNameExist(string name)
    {
        return dbMgr.QueryNameData(name);
    }
    //通过Session获得玩家信息
    public PlayerData GetPlayerDataBySession(ServerSession session)
    {
        if (onlineSessionDic.TryGetValue(session, out PlayerData data))
        {
            return data;
        }
        return null;
    }
    //更新数据库的玩家信息
    public bool UpdatePlayerData(int id, PlayerData data)
    {
        return dbMgr.UpdataPlayerData(id, data);
    }
    //玩家下线移除缓存
    public void AccountOffline(ServerSession session)
    {

        foreach (var item in onlineAccountDic)
        {
            if (item.Value == session)
            {
                onlineAccountDic.Remove(item.Key);
                return;
            }
        }
        PECommon.Log("Account Not Online...", LogType.Warn);
    }
}

