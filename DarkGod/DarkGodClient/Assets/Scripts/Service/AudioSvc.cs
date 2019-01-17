/****************************************************
    文件：AudioSvc.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/1/9 15:26:9
	功能：声音服务
*****************************************************/

using UnityEngine;

public class AudioSvc : MonoBehaviour
{
    public static AudioSvc Instance;

    [SerializeField] private AudioSource BGAudio;
    [SerializeField] private AudioSource UIAudio;
    public void InitAudioSvc()
    {
        Instance = this;
    }

    public void PlayBGM(string clipName, bool isLoop = true)
    {
        if (BGAudio.clip == null || BGAudio.clip.name != clipName)
        {
            BGAudio.clip = ResSvc.Instance.LoadAudioClip("ResAudio/" + clipName, true);
            BGAudio.loop = isLoop;
            BGAudio.Play();
        }
    }

    public void PlayUIAudio(string clipName)
    {
        UIAudio.clip = ResSvc.Instance.LoadAudioClip("ResAudio/" + clipName, true);
        UIAudio.Play();
    }
}