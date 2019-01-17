/****************************************************
    文件：Protocol.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 12:04
	功能：网络协议
*****************************************************/

using System;
using PENet;

namespace Protocol
{
    [Serializable]
    public class NetMsg : PEMsg
    {
        public string text;
        public ReqLogin reqLogin;
        public RspLogin rspLogin;
    }
    [Serializable]
    public class ReqLogin
    {
        public string account;
        public string password;
    }

    [Serializable]
    public class RspLogin
    {
        public PlayerData playerData;
    }

    [Serializable]
    public class PlayerData
    {
        public int id;
        public string name;
        public int level;
        public int exp;
        public int power;
        public int coin;
        public int diamond;

        public PlayerData(int id, string name, int level, int exp, int power, int coin, int diamond)
        {
            this.id = id;
            this.name = name;
            this.level = level;
            this.exp = exp;
            this.power = power;
            this.coin = coin;
            this.diamond = diamond;
        }
    }

    public enum CMD
    {
        None = 1000,
        ReqLogin,//接受
        RspLogin //响应
    }

    public enum Error
    {
        None = 2000,
        AccountIsOnline,
        WrongPass
    }

    public class SerCfg
    {
        public const string sevIP = "127.0.0.1";
        public const int sevPort = 17666;
    }


}
