/****************************************************
    文件：WindowRoot.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/1/9 11:40:58
	功能：窗口基类
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WindowRoot : MonoBehaviour
{
    protected ResSvc res = null;
    protected AudioSvc audio = null;
    protected NetSvc net = null;

    public void SetWndState(bool isActive = true)
    {
        if (gameObject.activeSelf != isActive)
        {
            SetActive(gameObject, isActive);
        }

        if (isActive) { InitWnd(); }
        else { CleanWnd(); }
    }

    protected virtual void InitWnd()
    {
        res = ResSvc.Instance;
        audio = AudioSvc.Instance;
        net = NetSvc.Instance;
    }

    protected virtual void CleanWnd()
    {
        res = null;
        audio = null;
    }

    #region SetFuncation
    protected void SetActive(GameObject go, bool isActive = true)
    {
        go.SetActive(isActive);
    }
    protected void SetActive(Transform tran, bool isActive = true)
    {
        tran.gameObject.SetActive(isActive);
    }
    protected void SetActive(RectTransform rtran, bool isActive = true)
    {
        rtran.gameObject.SetActive(isActive);
    }
    protected void SetActive(Text text, bool isActive = true)
    {
        text.gameObject.SetActive(isActive);
    }
    protected void SetActive(Image img, bool isActive = true)
    {
        img.gameObject.SetActive(isActive);
    }

    protected void SetText(Text text, string context = "")
    {
        text.text = context;
    }
    protected void SetText(Text text, int num = 0)
    {
        text.text = num.ToString();
    }
    protected void SetText(InputField text, string context = "")
    {
        text.text = context;
    }
    protected void SetText(InputField text, int num = 0)
    {
        text.text = num.ToString();
    }
    #endregion

    protected T GetOrAddComponent<T>(GameObject obj) where T : Component
    {
        T t = obj.GetComponent<T>();
        if (t == null) { t = obj.AddComponent<T>(); }
        return t;
    }

    #region ClickEvent
    protected void OnClinckDown(GameObject obj, Action<PointerEventData> callback)
    {
        PEListener listener = GetOrAddComponent<PEListener>(obj);
        listener.downCallback = callback;
    }
    protected void OnClinckUp(GameObject obj, Action<PointerEventData> callback)
    {
        PEListener listener = GetOrAddComponent<PEListener>(obj);
        listener.upCallback = callback;
    }
    protected void OnDrag(GameObject obj, Action<PointerEventData> callback)
    {
        PEListener listener = GetOrAddComponent<PEListener>(obj);
        listener.dragCallback = callback;
    }
    #endregion
}