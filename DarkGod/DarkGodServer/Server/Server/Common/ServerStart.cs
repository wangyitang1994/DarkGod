/****************************************************
    文件：ServerStart.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 10:48
	功能：服务器入口
*****************************************************/


namespace Server
{
    class ServerStart
    {

        static void Main(string[] args)
        {

            ServerRoot.Instance.Init();
            while (true)
            {
                NetSvc.Instance.Update();
            }
        }


    }

}
