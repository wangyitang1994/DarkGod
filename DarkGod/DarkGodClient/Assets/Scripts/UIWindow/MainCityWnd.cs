/****************************************************
    文件：MainCityWnd.cs
	作者：Plane
    邮箱: 1785275942@qq.com
    日期：2019/1/29 16:19:2
	功能：主城窗口控制
*****************************************************/

using UnityEngine;
using UnityEngine.UI;
using Protocol;
using UnityEngine.EventSystems;

public class MainCityWnd : WindowRoot
{
    [SerializeField] private Text textFight;
    [SerializeField] private Text textPower;
    [SerializeField] private Text textLevel;
    [SerializeField] private Text textExp;
    [SerializeField] private Image imgPower;
    [SerializeField] private Transform expTrans;
    [SerializeField] private Button btnFlod;
    [SerializeField] private Animation anim;
    [SerializeField] private GameObject touchField;
    [SerializeField] private GameObject bgDir;
    [SerializeField] private GameObject bgPoint;
    private bool isFlod;
    private Vector2 startPos;
    private float defaultDis;

    protected override void InitWnd()
    {
        base.InitWnd();
        RefreshUI();
        InitPointEvent();
        ButtonAddListener();
    }

    private void RefreshUI()
    {
        PlayerData data = GameRoot.Instance.PlayerData;
        int lv = data.level;
        int power = data.power;
        SetText(textFight, PECommon.GetFight(data));
        SetText(textPower, "体力:" + power + "/" + PECommon.GetPowerLimit(lv));
        SetText(textLevel, lv.ToString());
        imgPower.fillAmount = (float)power / PECommon.GetPowerLimit(lv);
        ExpAdaption();
        float expPrg = (float)data.exp / PECommon.GetLevelUpValue(lv) * 100;

        SetText(textExp, string.Format("{0:F1}%", expPrg));
        int index = (int)expPrg / 10;
        for (int i = 0; i < expTrans.childCount; i++)
        {
            Image img = expTrans.GetChild(i).GetComponent<Image>();
            if (i < index) { img.fillAmount = 1; }
            else if (i == index) { img.fillAmount = expPrg % 10 / 10; }
            else { img.fillAmount = 0; }
        }

    }
    //经验条自适应
    private void ExpAdaption()
    {
        float screenRate = Constants.ScreenStandardHieght / Screen.height;
        GridLayoutGroup grid = expTrans.GetComponent<GridLayoutGroup>();
        float width = (Screen.width * screenRate - 180) / 10;
        grid.cellSize = new Vector2(width, 7);
    }

    private void ButtonAddListener()
    {
        //菜单折叠按钮
        btnFlod.onClick.AddListener(() =>
        {
            audio.PlayUIAudio(Constants.UIExternBtn);
            AnimationClip clip = isFlod ? anim.GetClip("OpenMenu") : anim.GetClip("CloseMenu");
            anim.Play(clip.name);
            isFlod = !isFlod;
        });
    }
    //
    private void InitPointEvent()
    {
        SetActive(bgDir, false);
        defaultDis = Screen.height / Constants.ScreenStandardHieght * Constants.JoyStickMaxDis;//计算默认距离
        OnClinckDown(touchField, (PointerEventData evt) =>
         {
             SetActive(bgDir);
             SetActive(bgPoint);
             bgDir.transform.position = startPos = evt.position;
         });
        OnClinckUp(touchField, (PointerEventData evt) =>
        {
            bgPoint.transform.localPosition = Vector2.zero;
            SetActive(bgDir, false);
            SetActive(bgPoint, false);
        });
        OnDrag(touchField, (PointerEventData evt) =>
        {
            Vector2 dir = evt.position - startPos;
            if(dir.magnitude <= defaultDis)
            {
                bgPoint.transform.position = evt.position;
            }
            else
            {
                bgPoint.transform.localPosition = Vector2.ClampMagnitude(dir, defaultDis);
            }
            print(dir.normalized);
        });
    }
    //private void Update()
    //{
    //    RefreshUI();
    //}
}