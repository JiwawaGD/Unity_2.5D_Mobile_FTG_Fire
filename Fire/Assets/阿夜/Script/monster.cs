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

     void Start()
    {
        //status = Status.walk;
        status = Status.idle;

        if (this.transform.GetComponent<SpriteRenderer>().flipX)
        {
            face = Face.Right;
        }
        else
        {
            face = Face.Left;
        }
        myTransform = this.transform;
    }

    void Update()
    {
        float deltaTime = Time.deltaTime;
        switch (status)
        {
            case Status.idle:
                break;
            case Status.walk:
                switch (face)
                {
                    case Face.Right:
                        myTransform.position += new Vector3(speed* deltaTime, 0, 0);
                        break;
                    case Face.Left:
                        myTransform.position -= new Vector3(speed * deltaTime, 0, 0);
                        break;
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