/****************************************************
    文件：LoginSys.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 10:49
	功能：登陆系统
*****************************************************/

using System.Runtime.CompilerServices;
using Protocol;

class LoginSys : Singleton<LoginSys>
{
    CacheSvc cache = null;

    public void Init()
    {
        cache = CacheSvc.Instance;
    }

    public void ReqLogin(MsgPack pack)
    {
        NetMsg msg = new NetMsg()
        {
            cmd = (int)CMD.RspLogin
        };
        ReqLogin reqLogin = pack.netMsg.reqLogin;
        //是否已经登陆
        if (cache.IsAccountOnline(reqLogin.account))
        {
            msg.err = (int)Error.AccountIsOnline;
        }
        else
        {
            //账号是否存在
            PlayerData data = cache.GetPlayerData(reqLogin.account, reqLogin.password);
            if (data != null)
            {
                msg.rspLogin = new RspLogin()
                {
                    playerData = data
                };
                cache.AccountOnline(reqLogin.account, pack.serSession, data);
            }
            else
            {
                msg.err = (int)Error.WrongPass;
            }

        }
        pack.serSession.SendMsg(msg);
    }

    public void ReqRename(MsgPack pack)
    {
        NetMsg msg = new NetMsg()
        {
            cmd = (int)CMD.RspRename
        };
        ReqRename reqRename = pack.netMsg.reqRename;
        //玩家昵称是否存在
        if (!cache.IsNameExist(reqRename.name))
        {
            PECommon.Log("不存在");
            PlayerData data = cache.GetPlayerDataBySession(pack.serSession);
            data.name = reqRename.name;
            //数据库更新是否成功
            if (cache.UpdatePlayerData(data.id, data))
            {
                PECommon.Log("成功");
                msg.rspRename = new RspRename()
                {
                    name = data.name
                };
            }
            else
            {
                msg.err = (int)Error.UpdateDBError;
            }
        }
        else
        {
            msg.err = (int)Error.NameIsExist;
        }
        pack.serSession.SendMsg(msg);
    }
}


