/****************************************************
    文件：loadingWnd.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/1/8 9:41:8
	功能：读取窗口控制
*****************************************************/

using UnityEngine;
using UnityEngine.UI;

public class LoadingWnd : WindowRoot
{
    [SerializeField] private Text tipText;
    [SerializeField] private Text prgText;
    [SerializeField] private Slider progressBar;

    protected override void InitWnd()
    {
        base.InitWnd();
        SetText(tipText, "随机一条提示");
    }

    public void SetPrg(float prg)
    {
        SetText(prgText, prg * 100 + "%");
        progressBar.value = prg;
    }
}