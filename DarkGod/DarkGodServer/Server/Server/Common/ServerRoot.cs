/****************************************************
    文件：ServerRoot.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/01/12 10:47
	功能：服务器管理
*****************************************************/


    class ServerRoot : Singleton<ServerRoot>
    {
        private int sessionID;

        public void Init()
        {
            NetSvc.Instance.Init();
            CacheSvc.Instance.Init();
            DataBaseMgr.Instance.Init();
            LoginSys.Instance.Init();
        }

        public int GetSessionID()
        {
            if (sessionID >= int.MaxValue)
            {
                sessionID = 0;
            }
            return sessionID + 1;
        }
    }

