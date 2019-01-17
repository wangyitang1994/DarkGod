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
        PECommon.Log("DataBaseMgr Init...");
        string constr = "server=localhost;user='root';password=;database=darkgod;charset=utf8";
        conn = new MySqlConnection(constr);
        conn.Open();
        QueryPlayerData("MYMY", "123456");
    }

    public PlayerData QueryPlayerData(string account, string password)
    {
        bool isExist = false;
        PlayerData data = null;
        try
        {
            cmd = new MySqlCommand("select * from accountinfo where account=@acct", conn);
            cmd.Parameters.AddWithValue("acct", account);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read() && reader.GetString("password") == password)
            {
                isExist = true;
                int id = reader.GetInt32("id");
                string name = reader.GetString("name");
                int level = reader.GetInt32("level");
                int exp = reader.GetInt32("exp");
                int power = reader.GetInt32("power");
                int coin = reader.GetInt32("coin");
                int diamond = reader.GetInt32("diamond");
                data = new PlayerData(id, name, level, exp, power, coin, diamond);
                PECommon.Log("Return PlayerData...");
            }
            else
            {
                PECommon.Log("Account Or Password is Wrong...");
            }
            reader.Close();
        }
        catch (Exception e)
        {
            PECommon.Log("QueryPlayerData Error:" + e, LogType.Error);
        }
        finally
        {
            if (!isExist)
            {
                int id = -1;
                string name = "";
                int level = 1;
                int exp = 0;
                int power = 120;
                int coin = 3000;
                int diamond = 0;
                data = new PlayerData(id, name, level, exp, power, coin, diamond);
                data.id = InsertNewAcctData(account, password, data);
            }
        }
        return data;
    }

    private int InsertNewAcctData(string acct, string pass, PlayerData data)
    {
        try
        {
            cmd = new MySqlCommand("insert into accountinfo set account=@acct,password=@pass,name=@name,level=@level,exp=@exp,power=@power,coin=@coin,diamond=@diamond", conn);
            cmd.Parameters.AddWithValue("acct", acct);
            cmd.Parameters.AddWithValue("pass", pass);
            cmd.Parameters.AddWithValue("name", data.name);
            cmd.Parameters.AddWithValue("level", data.level);
            cmd.Parameters.AddWithValue("exp", data.exp);
            cmd.Parameters.AddWithValue("power", data.power);
            cmd.Parameters.AddWithValue("coin", data.coin);
            cmd.Parameters.AddWithValue("diamond", data.diamond);
            cmd.ExecuteNonQuery();
            return (int)cmd.LastInsertedId;
        }
        catch (Exception e)
        {
            PECommon.Log("InsertNewAcctData Error:" + e, LogType.Error);
        }
        return -1;
    }
}

