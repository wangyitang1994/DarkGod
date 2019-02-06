/****************************************************
    文件：PEListener.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/2/2 21:15:42
	功能：触摸监听
*****************************************************/

using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PEListener : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IDragHandler
{
    public Action<PointerEventData> dragCallback;
    public Action<PointerEventData> downCallback;
    public Action<PointerEventData> upCallback;

    public void OnDrag(PointerEventData eventData)
    {
        if (dragCallback != null) { dragCallback(eventData); }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (downCallback != null) { downCallback(eventData); }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (upCallback != null) { upCallback(eventData); }
    }
}