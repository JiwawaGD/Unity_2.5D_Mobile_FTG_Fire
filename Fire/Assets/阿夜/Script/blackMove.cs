using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 小黑炭移動
/// </summary>
public class blackMove : MonoBehaviour
{
    //玩家走到固定地點生成小怪
    //在地板上放觸發器,當玩家碰到時生成小怪
    //生成時間隨機【0,2】

    //小黑炭創建時間
    float creatTime = 5f;
    //創建小黑炭
    GameObject black;


    void Start()
    {
        ////倒數計時
        //creatTime -= Time.deltaTime;
        //if (creatTime<=0)
        //{
        //    creatTime = Random.Range(0, 3);
        //    GameObject obj = (GameObject)Resources.Load("");
        //}
    }

    void Update()
    {
        
    }
}
