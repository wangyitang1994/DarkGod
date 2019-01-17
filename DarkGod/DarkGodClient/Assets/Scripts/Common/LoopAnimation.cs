/****************************************************
    文件：LoopAnimation.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/1/9 16:14:10
	功能：循环动画
*****************************************************/

using UnityEngine;

public class LoopAnimation : MonoBehaviour
{
    private Animation anim;
    private void Start()
    {
        anim = GetComponent<Animation>();
        if (anim.clip != null)
        {
            InvokeRepeating("Play",0,20);
        }
    }

    private void Play()
    {
        anim.Play();
    }

}