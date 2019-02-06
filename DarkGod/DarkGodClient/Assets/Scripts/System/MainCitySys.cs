/****************************************************
    文件：MainCitySys.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2019/1/29 16:41:57
	功能：主城系统
*****************************************************/

using UnityEngine;

public class MainCitySys : SystemRoot 
{
    public static MainCitySys Instance;

    [SerializeField] private MainCityWnd mainCityWnd;
    public MainCityWnd MainCityWnd
    {
        get { return mainCityWnd; }
    }

    public override void InitSystem()
    {
        base.InitSystem();
        Instance = this;
    }

    public void EnterCity()
    {
        resSvc.AsynLoadingScene(Constants.MainCity, () =>
        {
            //TODO 加载人物
            mainCityWnd.SetWndState();
            audioSvc.PlayBGM(Constants.MainCityBGM);
        });
    }
}