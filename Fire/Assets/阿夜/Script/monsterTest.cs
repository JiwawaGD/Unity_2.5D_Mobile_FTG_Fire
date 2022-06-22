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
    //動畫
    Animator ani;
    //位置
    Vector3 pos;
    //玩家跟敵人之間的距離
    float distance;

    void Start()
    {
        pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        timeMove += Time.deltaTime;
        if (timeMove>=4)
        {
            canMove = true;
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            //假設我在x軸-3到3之間左右循環移動
            if (movingRight)
            {
                //右移
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
                ani.SetBool("walk", true);

                //如果我移到了3 那麼接下來就是左移,所以把右移設為false
                if (transform.position.x - pos.x >= 5 )
                {
                    canMove = false;
                    timeMove = 0;
                    movingRight = false;
                    ani.SetBool("walk", false);
                    //if (pos)s
                    //{
                    //    if (Mathf.Abs(Transform.position.x - pos.position.x) < 10f)
                    //    {
                    //        ani.SetBool("walk", false);
                    //    }
                    //}
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
        ////近距離 觸發 敵人攻擊
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

        ////用 自己的位置 減 敵人的位置
        //Vector2 toVector = transform.position - colliders[i].gameObject.transform.position;
        ////取得夾角
        //float nowAngle = Mathf.Atan2(toVector.y, toVector.x) * Mathf.Rad2Deg;
        ////轉90
        //nowAngle = nowAngle - 90;
        ////面向方向
        //colliders[i].gameObject.transform.rotation = Quaternion.Euler(0, 0, nowAngle);
    }
}
