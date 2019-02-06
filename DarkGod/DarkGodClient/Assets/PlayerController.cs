/****************************************************
    文件：PlayerController.cs
	作者：Wyt
    邮箱: 1217805934@qq.com
    日期：2019/2/5 22:19:35
	功能：玩家控制器
*****************************************************/

using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    private Animator anim;

    private float targetValue;
    private bool isMove;
    private IEnumerator currentCoroutine;

    private Vector2 dir;
    private Vector2 Dir
    {
        get { return dir; }
        set
        {
            if (value != dir)
            {
                if (value == Vector2.zero)
                {
                    dir = value;
                    isMove = false;
                    targetValue = 0;
                }
                else
                {
                    dir = value;
                    isMove = true;
                    targetValue = 1;
                }
            }
        }
    }
    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Dir = new Vector2(h, v).normalized;
        SetBlend(5);
        if (isMove)
        {
            SetDir();
            Move();
        }

    }
    private void SetDir()
    {
        float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1));
        transform.localEulerAngles = new Vector3(0, angle, 0);
    }
    private void Move()
    {
        cc.Move(transform.forward * Constants.PlayerSpeed * Time.fixedDeltaTime);
    }
    private void SetBlend(float acc)
    {
        float blend = anim.GetFloat("Move");
        if (blend != targetValue)
        {
            if (Mathf.Abs(blend - targetValue) < acc * Time.fixedDeltaTime)
            {
                blend = targetValue;
            }
            else
            {
                blend = Mathf.MoveTowards(blend, targetValue, acc * Time.fixedDeltaTime);
            }
            anim.SetFloat("Move", blend);
        }
    }

}