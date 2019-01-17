/****************************************************
    文件：PECommon.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 12:05
	功能：服务端和客户端公用类
*****************************************************/

using PENet;

namespace Protocol
{
    public enum LogType
    {
        None = 0,
        Warn = 1,
        Error = 2,
        Info = 3
    }
    public class PECommon
    {
        public static void Log(string msg = "", LogType type = LogType.None)
        {
            LogLevel lv = (LogLevel)type;
            PETool.LogMsg(msg, lv);
        }
    }
}
