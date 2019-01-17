/****************************************************
    文件：ServerRoot.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 10:47
	功能：服务器管理
*****************************************************/

namespace Server
{
    class ServerRoot : Singleton<ServerRoot>
    {
        public void Init()
        {
            NetSvc.Instance.Init();
            CacheSvc.Instance.Init();
            DataBaseMgr.Instance.Init();
            LoginSys.Instance.Init();
        }
    }
}
