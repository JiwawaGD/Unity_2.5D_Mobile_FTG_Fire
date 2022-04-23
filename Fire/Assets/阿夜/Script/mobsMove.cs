using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

/// <summary>
/// 小黑炭生成
/// </summary>
public class mobsMove : MonoBehaviour
{
    //要生成的小怪【 小黑炭 】
    public GameObject Mobs;
    //小怪生成總數量
    public int mobsNum = 10;
    //生成怪物的時間間隔
    public float mobsTime = 3f;
    //生成怪物計時器
    public int mobsCounter;
    //玩家
    private GameObject player;



    void Start()
    {
        //生成怪物計時器
        //player = GetComponent<GameObject>();
        //player = GameObject.Find("玩家");
        player = GameObject.FindGameObjectWithTag("Player");
        mobsCounter = 0;
        InvokeRepeating("CreatEnemy", 0.5f, mobsTime);
    }

    void Update()
    {

    }

    void CreatEnemy()
    {
        if (player.GetComponent<Player>().currentHp > 0)
        {
            Instantiate(Mobs, this.transform.position, UnityEngine.Quaternion.identity);
            mobsCounter++;
            int trmobsCounterue = 0;
            if (trmobsCounterue == mobsNum)
            {
                CancelInvoke();
            }
        }
        else { CancelInvoke(); }


    }
}
