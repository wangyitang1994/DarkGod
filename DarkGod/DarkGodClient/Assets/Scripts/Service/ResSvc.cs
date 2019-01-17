using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Protocol;
using UnityEngine;
using UnityEngine.SceneManagement;
using LogType = Protocol.LogType;

public class ResSvc : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();
    private Dictionary<string, XmlNodeList> xmlDic = new Dictionary<string, XmlNodeList>();


    public static ResSvc Instance;
    private RandomName rdName;

    public void InitResSvc()
    {
        Instance = this;
        //初始化加载服务
        InitRdName();
    }
    //异步加载场景
    public void AsynLoadingScene(string sceneName, Action callBack)
    {
        GameRoot.Instance.LoadingWnd.gameObject.SetActive(true);
        GameRoot.Instance.LoadingWnd.SetWndState();
        AsyncOperation asyn = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(UpdateLoadingProgress(asyn, callBack));
    }
    IEnumerator UpdateLoadingProgress(AsyncOperation asyn, Action callBack)
    {
        while (!asyn.progress.Equals(1))
        {
            GameRoot.Instance.LoadingWnd.SetPrg(asyn.progress);
            yield return null;
        }
        GameRoot.Instance.LoadingWnd.SetWndState(false);
        callBack();
    }
    //加载声音
    public AudioClip LoadAudioClip(string path, bool cache = false)
    {
        AudioClip clip = null;
        if (!audioDic.TryGetValue(path, out clip))
        {
            clip = Resources.Load<AudioClip>(path);
            if (cache) { audioDic.Add(path, clip); }
        }
        return clip;
    }

    private void InitRdName()
    {
        XmlNodeList root = LoadXML(PathDefine.rdName);
        rdName = new RandomName();
        foreach (XmlElement ele in root)
        {
            foreach (XmlElement xe in ele.ChildNodes)
            {
                switch (xe.Name)
                {
                    case "surname":
                        rdName.surnameList.Add(xe.InnerText);
                        break;
                    case "man":
                        rdName.manList.Add(xe.InnerText);
                        break;
                    case "woman":
                        rdName.womanList.Add(xe.InnerText);
                        break;
                }
            }
        }
    }
    //加载XML
    public XmlNodeList LoadXML(string path, bool cache = false)
    {
        XmlNodeList xmlNodeList = null;
        TextAsset ta = Resources.Load<TextAsset>(path);
        if (!ta)
        {
            PECommon.Log("xml file:" + path + " not exist",LogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(ta.ToString());
            xmlNodeList = doc["root"].ChildNodes;
            if (cache) { xmlDic.Add(path, xmlNodeList); }
        }
        return xmlNodeList;
    }

    public string GetRandomName(bool isMan = true)
    {
        string name = rdName.surnameList[PETools.RDInt(rdName.surnameList.Count - 1)];
        name += isMan? rdName.manList[PETools.RDInt(rdName.manList.Count - 1)]: rdName.womanList[PETools.RDInt(rdName.womanList.Count - 1)];
        return name;
    }
}
