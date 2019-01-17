/****************************************************
    文件：LoginSys.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 10:49
	功能：登陆系统
*****************************************************/

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
}


