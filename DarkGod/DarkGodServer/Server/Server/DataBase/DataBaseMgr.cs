/****************************************************
    文件：DataBaseMgr.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/16 15:07
	功能：数据库管理
*****************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Protocol;

class DataBaseMgr : Singleton<DataBaseMgr>
{
    private MySqlConnection conn = null;
    private MySqlCommand cmd = null;

    public void Init()
    {
        string constr = "server=192.168.1.102;user='root';password=;database=darkgod;charset=utf8";
        conn = new MySqlConnection(constr);
        conn.Open();
        PECommon.Log("DataBaseMgr Init...");
    }

    public PlayerData QueryPlayerData(string account, string password)
    {
        bool isExist = false;
        PlayerData data = null;
        MySqlDataReader reader = null;
        try
        {
            cmd = new MySqlCommand("select * from accountinfo where account=@acct", conn);
            cmd.Parameters.AddWithValue("acct", account);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetString("password") == password)
                {
                    isExist = true;
                    int id = reader.GetInt32("id");
                    string name = reader.GetString("name");
                    int level = reader.GetInt32("level");
                    int exp = reader.GetInt32("exp");
                    int power = reader.GetInt32("power");
                    int coin = reader.GetInt32("coin");
                    int diamond = reader.GetInt32("diamond");

                    int hp = reader.GetInt32("hp");
                    int ad = reader.GetInt32("ad");
                    int ap = reader.GetInt32("ap");
                    int addef = reader.GetInt32("addef");
                    int apdef = reader.GetInt32("apdef");
                    int dodge = reader.GetInt32("dodge");
                    int pierce = reader.GetInt32("pierce");
                    int critical = reader.GetInt32("critical");
                    data = new PlayerData(id, name, level, exp, power, coin, diamond, hp, ad, ap, addef, apdef, dodge, pierce, critical);
                    PECommon.Log("Return PlayerData...");
                }
                else
                {
                    PECommon.Log("Account Or Password is Wrong...");
                    return null;
                }
            }
        }
        catch (Exception e)
        {
            PECommon.Log("QueryPlayerData Error:" + e, LogType.Error);
        }
        finally
        {
            reader.Close();
        }
        if (!isExist)
        {
            int id = -1;
            string name = "";
            int level = 1;
            int exp = 0;
            int power = 120;
            int coin = 3000;
            int diamond = 0;
            int hp = 2000;
            int ad = 275;
            int ap = 265;
            int addef = 67;
            int apdef = 43;
            int dodge = 7;
            int pierce = 5;
            int critical = 2;
            data = new PlayerData(id, name, level, exp, power, coin, diamond, hp, ad, ap, addef, apdef, dodge, pierce, critical);
            data.id = InsertNewAcctData(account, password, data);
        }
        return data;
    }

    private int InsertNewAcctData(string acct, string pass, PlayerData data)
    {
        try
        {
            cmd = new MySqlCommand("insert into accountinfo set account=@acct,password=@pass,name=@name,level=@level,exp=@exp,power=@power,coin=@coin,diamond=@diamond" +
                ",hp=@hp,ad=@ad,ap=@ap,addef=@addef,apdef=@apdef,dodge=@dodge,pierce=@pierce,critical=@critical", conn);
            cmd.Parameters.AddWithValue("acct", acct);
            cmd.Parameters.AddWithValue("pass", pass);
            cmd.Parameters.AddWithValue("name", data.name);
            cmd.Parameters.AddWithValue("level", data.level);
            cmd.Parameters.AddWithValue("exp", data.exp);
            cmd.Parameters.AddWithValue("power", data.power);
            cmd.Parameters.AddWithValue("coin", data.coin);
            cmd.Parameters.AddWithValue("diamond", data.diamond);

            cmd.Parameters.AddWithValue("hp", data.hp);
            cmd.Parameters.AddWithValue("ad", data.ad);
            cmd.Parameters.AddWithValue("ap", data.ap);
            cmd.Parameters.AddWithValue("addef", data.addef);
            cmd.Parameters.AddWithValue("apdef", data.apdef);
            cmd.Parameters.AddWithValue("dodge", data.dodge);
            cmd.Parameters.AddWithValue("pierce", data.pierce);
            cmd.Parameters.AddWithValue("critical", data.critical);

            cmd.ExecuteNonQuery();
            return (int)cmd.LastInsertedId;
        }
        catch (Exception e)
        {
            PECommon.Log("InsertNewAcctData Error:" + e, LogType.Error);
        }
        return -1;
    }

    public bool QueryNameData(string name)
    {
        MySqlDataReader reader = null;
        try
        {
            cmd = new MySqlCommand("select name from accountinfo where name = @name", conn);
            cmd.Parameters.AddWithValue("name", name);
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }
        }
        catch (Exception e)
        {
            PECommon.Log("QueryNameData Error:" + e, LogType.Error);
        }
        finally
        {
            reader.Close();
        }
        return false;
    }

    public bool UpdataPlayerData(int id, PlayerData data)
    {
        try
        {
            cmd = new MySqlCommand("update accountinfo set name=@name,level=@level,exp=@exp,power=@power,coin=@coin,diamond=@diamond" +
                ",hp=@hp,ad=@ad,ap=@ap,addef=@addef,apdef=@apdef,dodge=@dodge,pierce=@pierce,critical=@critical where id=@id", conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("name", data.name);
            cmd.Parameters.AddWithValue("level", data.level);
            cmd.Parameters.AddWithValue("exp", data.exp);
            cmd.Parameters.AddWithValue("power", data.power);
            cmd.Parameters.AddWithValue("coin", data.coin);
            cmd.Parameters.AddWithValue("diamond", data.diamond);

            cmd.Parameters.AddWithValue("hp", data.hp);
            cmd.Parameters.AddWithValue("ad", data.ad);
            cmd.Parameters.AddWithValue("ap", data.ap);
            cmd.Parameters.AddWithValue("addef", data.addef);
            cmd.Parameters.AddWithValue("apdef", data.apdef);
            cmd.Parameters.AddWithValue("dodge", data.dodge);
            cmd.Parameters.AddWithValue("pierce", data.pierce);
            cmd.Parameters.AddWithValue("critical", data.critical);
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception e)
        {
            PECommon.Log("UpdataPlayerData Error:" + e, LogType.Error);
        }
        return false;
    }
}

