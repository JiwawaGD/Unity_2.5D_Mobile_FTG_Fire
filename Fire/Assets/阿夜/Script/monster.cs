using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// 小怪
/// </summary>
public class monster : MonoBehaviour
{
    //小怪等待、走路
    public enum Status { idle,walk};
    public Status status;

    //小怪面向位置
    public enum Face { Right,Left};
    public Face face;

    //速度
    public float speed;
    //位置
    private Transform myTransform;

    //腳色位置
    public Transform playerPos;
    private SpriteRenderer spr;

     void Start()
    {
        status = Status.walk;
        //status = Status.idle;
        spr = this.transform.GetComponent<SpriteRenderer>();
        if (spr.flipX)
        {
            face = Face.Right;
        }
        else
        {
            face = Face.Left;
        }
        myTransform = this.transform;
        if (GameObject.Find("吉娃娃_Player_1") != null)
        {
           playerPos = GameObject.Find("吉娃娃_Player_1").transform;
        }
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        switch (status)
        {
            case Status.idle:
                if (playerPos)
                {
                    if (Mathf.Abs(myTransform.position.x - playerPos.position.x) < 10f)
                    {
                        status = Status.walk;
                    }
                }
                break;
            case Status.walk:
                if (playerPos)
                {
                    if (myTransform.position.x >= playerPos.position.x)
                    {
                        spr.flipX = false;
                        face = Face.Left;
                    }
                    else
                    {
                        spr.flipX = true;
                        face = Face.Right;
                    }
                }
                switch (face)
                {
                    case Face.Right:
                        myTransform.position += new Vector3(speed* deltaTime, 0, 0);
                        break;
                    case Face.Left:
                        myTransform.position -= new Vector3(speed * deltaTime, 0, 0);
                        break;
                }
                if (playerPos)
                {
                    if (Mathf.Abs(myTransform.position.x - playerPos.position.x) >= 10f)
                    {
                        status = Status.idle;
                    }
                }
                break;
        }

    }

    ////小怪預製物
    //public GameObject enemys;
    ////小黑炭左右偵測
    //public Transform rightPoint, leftPoint;
    ////小黑炭左右移動速度
    //[Header("小黑炭左右移動速度"),Range(0,1000)]
    //public float enemysSpeed;
    ////鋼體
    //private Rigidbody rb;
    ////臉部朝向左右
    //private bool faceLeft = true;
    ////小怪數量
    //int count;
    ////小怪計時
    //float enemysTime;


    //void Start()
    //{
    //    transform.DetachChildren();
    //    rb = GetComponent<Rigidbody>();
    //    moveMent();
    //}

    //void Update()
    //{

    //}

    ///// <summary>
    ///// 移動
    ///// </summary>
    //private void moveMent()
    //{
    //    if (faceLeft)
    //    {
    //        rb.velocity = new Vector2(-enemysSpeed, rb.velocity.y);
    //        //如果小黑炭當前位置小於左邊偵測位置
    //        if (transform.position.x < leftPoint.position.x)
    //        {
    //            transform.localScale = new Vector3(-1, 1, 1);
    //            faceLeft = false;
    //        }
    //    }
    //    else
    //    {
    //        rb.velocity = new Vector2(enemysSpeed, rb.velocity.y);
    //        //如果小黑炭當前位置小於右邊偵測位置
    //        if (transform.position.x > rightPoint.position.x)
    //        {
    //            transform.localScale = new Vector3(1, 1, 1);
    //            faceLeft = true;
    //        }
    //    }
    //}
}