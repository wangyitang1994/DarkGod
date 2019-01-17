/****************************************************
    文件：SystemRoot.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/1/9 16:7:53
	功能：系统基类
*****************************************************/

using UnityEngine;

public class SystemRoot : MonoBehaviour
{
    protected ResSvc resSvc = null;
    protected AudioSvc audioSvc = null;

    public virtual void InitSystem()
    {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
    }
}