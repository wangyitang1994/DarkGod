/****************************************************
    文件：DynamicWnd.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/1/9 17:15:23
	功能：Nothing
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicWnd : WindowRoot
{
    [SerializeField] private Text tipText;
    [SerializeField] private Animation anim;
    private Queue<string> tipsQueue = new Queue<string>();
    private IEnumerator coroutine = null;
    private string preTip;

    protected override void InitWnd()
    {
        base.InitWnd();
    }


    public void AddTips(string tip)
    {
        if (tip == preTip) return;
        preTip = tip;
        SetActive(tipText);
        tipsQueue.Enqueue(tip);
        if (coroutine == null)
        {
            coroutine = ShowTips(anim.clip.length, () =>
               {
                   SetActive(tipText, false);
                   coroutine = null;
                   preTip = null;
               });
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator ShowTips(float clipLength, Action callback)
    {
        while (tipsQueue.Count > 0)
        {
            anim.PlayQueued("TipAnim");
            tipText.text = tipsQueue.Dequeue();
            yield return new WaitForSeconds(clipLength);
        }
        callback();

    }

}