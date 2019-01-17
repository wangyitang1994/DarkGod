/****************************************************
    文件：PETools.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/10 15:18
	功能：工具
*****************************************************/

using System;

public class PETools
{
    public static int RDInt(int min, int max, Random random = null)
    {
        if(random==null)random = new Random();
        int rdNum = random.Next(min, max + 1);
        return rdNum;
    }
    public static int RDInt(int max, Random random = null)
    {
        if (random == null) random = new Random();
        int rdNum = random.Next(0, max + 1);
        return rdNum;
    }
}

