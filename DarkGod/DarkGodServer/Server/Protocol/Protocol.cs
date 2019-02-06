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
        public ReqRename reqRename;
        public RspRename rspRename;
    }
    //登陆接收
    [Serializable]
    public class ReqLogin
    {
        public string account;
        public string password;
    }
    //登陆响应
    [Serializable]
    public class RspLogin
    {
        public PlayerData playerData;
    }
    //重命名接收
    [Serializable]
    public class ReqRename
    {
        public string name;
    }
    /// 重命名响应
    [Serializable]
    public class RspRename
    {
        public string name;
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
        public int hp;
        public int ad;
        public int ap;
        public int addef;
        public int apdef;
        public int dodge;
        public int pierce;
        public int critical;

        public PlayerData(int id, string name, int level, int exp, int power, int coin, int diamond, int hp, int ad, int ap, int addef, int apdef, int dodge, int pierce, int critical)
        {
            this.id = id;
            this.name = name;
            this.level = level;
            this.exp = exp;
            this.power = power;
            this.coin = coin;
            this.diamond = diamond;
            this.hp = hp;
            this.ad = ad;
            this.ap = ap;
            this.addef = addef;
            this.apdef = apdef;
            this.dodge = dodge;
            this.pierce = pierce;
            this.critical = critical;
        }
    }

    public enum CMD
    {
        None = 0,
        ReqLogin = 1001,//接受
        RspLogin,//响应
        ReqRename,
        RspRename,
    }

    public enum Error
    {
        None = 0,
        AccountIsOnline = 2001, //账号已登陆
        WrongPass,       //账号密码错误
        NameIsExist,     //重名
        UpdateDBError,   //数据库更新错误

    }

    public class SerCfg
    {
        public const string sevIP = "127.0.0.1";
        public const int sevPort = 17666;
    }


}
