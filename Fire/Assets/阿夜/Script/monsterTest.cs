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
    //要攻擊的目標
    public Transform Target;

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
        //計算玩家與敵人的距離
        float distsance = Vector3.Distance(transform.position, Target.position);
        //玩家與敵人的方向向量
        Vector3 temVec = Target.position - transform.position;
        //與玩家正前方做點積
        float forwardDistance = Vector3.Dot(temVec, transform.forward.normalized);
        if (forwardDistance > 0 && forwardDistance <= 10)
        {
            float rightDistance = Vector3.Dot(temVec, transform.right.normalized);
            if (Mathf.Abs(rightDistance) <= 3)
            {
                Debug.Log("進入攻擊範圍");
            }
        }
    }

    void FixedUpdate()
    {
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
    ////玩家進入到攻擊範圍時,小怪進行攻擊
    //public void OnTriggerEnter(Collision col)
    //{
    //    if (col.gameObject.tag == "Player" )
    //    {
    //        ani.SetBool("attacks", true);
    //    }
    //}
    ////當玩家離開時,停止攻擊
    //public void OnTriggerExit(Collision col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        ani.SetBool("attacks", false);
    //    }
    //}
}

