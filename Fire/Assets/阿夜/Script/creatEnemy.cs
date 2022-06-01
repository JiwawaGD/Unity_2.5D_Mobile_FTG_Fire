using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 生成點
/// </summary>
public class creatEnemy : MonoBehaviour
{
    [Header("小黑炭生成點")]
    //小怪預製物
    public GameObject enemys;
    //小怪數量
    int count;
    //小怪計時
    float enemysTime;

    void Update()
    {
        //計時器
        enemysTime += Time.deltaTime;
        //規定產生小怪數量
        if (count >= 0 && count < 5)
        {
            //小怪產生間隔
            if (enemysTime > 5.0f)
            {

                GameObject enemyObj = Instantiate(enemys, transform.position, Quaternion.identity) as GameObject;
                //時間清零
                enemysTime -= 9.0f;
                //記數
                count++;
            }
        }
        else
        {
            //小怪數量
            count = 10;
            //每波怪之間的間隔 其实就是用同一个计时器在不同的分支里执行只是合并了两个计时器
            if (enemysTime > 2)
            {
                //然後再計時結束後將時間和次數歸零
                count = 0;
                enemysTime -= 2;
            }
        }
    }
}
