using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class monsterTest : MonoBehaviour
{
    public float speed = 3f;
    private bool movingRight = true;
    public bool canMove;
    //計時器
    private float timeMove;
    //每次攻擊的時間
    private float attTime;
    //計算攻擊的次數
    private int attNum = 0;
    //計算技能施放次數
    private int skillNum;
    //計算玩家碰到的次數
    //private int hurtNum;

    //生命值
    private float HP = 100;

    //動畫
    Animator ani;
    ////動畫時長
    //public AnimatorClipInfo attInfo;
    //位置
    Vector3 pos;
    //要攻擊的目標
    public Transform Target;




    void Start()
    {
        pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ani = GetComponent<Animator>();
        skillNum = 0;
        attTime = 0;
        //hurtNum = 0;

    }

    void Update()
    {
        timeMove += Time.deltaTime;
        attTime += Time.deltaTime;
        Move();

    }

    void FixedUpdate()
    {

    }

    /// <summary>
    /// 移動方法
    /// </summary>
    void Move()
    {
        if (timeMove >= 4)
        {
            canMove = true;
        }
        //計算玩家與敵人的距離
        float distsance = Vector3.Distance(transform.position, Target.position);
        //Debug.Log(distsance);

        if (Mathf.Abs(distsance) <= 3)
        {
            attNum++;
            canMove = false;
            timeMove = 0;
            if (attTime > 1.283f)
            {
                Attack();
            }
        }
        //受傷
        if (Mathf.Abs(distsance) == 0)
        {
            float hurtNum = HP - 10;
            ani.SetTrigger("hurt");
            //死亡
            if (hurtNum ==0)
            {
                ani.SetBool("dead",true);
                Destroy(this.gameObject);
            }
        }

        if (canMove)
        {
            //在x軸-3到3之間左右循環移動
            if (movingRight)
            {
                //右移
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
                ani.SetBool("walk", true);

                //如果移到了3 那麼接下來就是左移,把右移設為false
                if (transform.position.x - pos.x >= 5)
                {
                    canMove = false;
                    timeMove = 0;
                    movingRight = false;
                    ani.SetBool("walk", false);
                }
            }
            else
            {
                //當我在左移，而且x軸座標到了-3，說明結束左移，開始右移
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);
                ani.SetBool("walk", true);

                //左移結束，右移開始，設置狀台維ture
                if (transform.position.x - pos.x <= -5)
                {
                    canMove = false;
                    timeMove = 0;
                    movingRight = true;
                    ani.SetBool("walk", false);
                }
            }
        }
    }



    /// <summary>
    /// 攻擊方法
    /// </summary>
    void Attack()
    {
        skillNum++;
        attTime = 0;
        if (skillNum >= 5)
        {
            ani.SetTrigger("skill");
            skillNum = 0;
        }
        else
        {
            timeMove = 0;
            canMove = false;
            ani.SetBool("walk", false);
            ani.SetTrigger("attack");
        }
    }

    ///// <summary>
    ///// 受傷
    ///// </summary>
    //void hurt()
    //{
    //    float distsance = vector3.distance(transform.position, target.position);
    //    if (distsance <= 0)
    //    {
    //        float hurtnum = hp - 10;
    //        ani.settrigger("hurt");
    //    }
    //}

    ///// <summary>
    ///// 死亡
    ///// </summary>
    //void dead()
    //{
    //    ani.setbool("dead", true);
    //}
    ///// <summary>
    ///// 施放技能
    ///// </summary>
    //void Skill()
    //{
    //    if (skillNum >= 5)
    //    {
    //        ani.SetTrigger("skill");
    //        skillNum = 0;
    //        timeMove = 0;
    //        canMove = false;
    //        ani.SetBool("walk", false);
    //    }
    //}
}

