using System.Collections;
using System.Collections.Generic;
using Protocol;
using UnityEngine;
using LogType = Protocol.LogType;

public class GameRoot : UnitySingleton<GameRoot>
{
    [SerializeField] private LoadingWnd loadingWnd;
    public LoadingWnd LoadingWnd {
        get { return loadingWnd; }
    }
   
    [SerializeField] private DynamicWnd dynamicWnd;
    public DynamicWnd DynamicWnd {
        get { return dynamicWnd; }
    }
    

    void Start()
    {
       
        InitUI();
        Init();
    }
    private void InitUI()
    {
        WindowRoot[] uiRoot = transform.Find("Canvas").GetComponentsInChildren<WindowRoot>();
        foreach (WindowRoot w in uiRoot)
        {
            w.SetWndState(false);
        }
        //dynamicWnd.SetWndState();
    }
    private void Init()
    {
        NetSvc net = GetComponent<NetSvc>();
        net.InitNetSvc();
        ResSvc res = GetComponent<ResSvc>();
        res.InitResSvc();
        AudioSvc audio = GetComponent<AudioSvc>();
        audio.InitAudioSvc();
        LoginSys login = GetComponent<LoginSys>();
        login.InitSystem();


        LoginSys.Instance.EnterLogin();
    }
}
