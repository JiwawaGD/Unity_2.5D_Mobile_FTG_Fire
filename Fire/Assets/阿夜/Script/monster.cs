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
    private Transform target;//設置追蹤目標的位置
    public float MoveSpeed = 2.5f;//敵人移動速度
    private NavMeshAgent navMeshAgent;//設置尋路組件
    private Animator animator;//定義變量動畫
    public float guaiwugogjili;
    private object guaiwugongjili;

    void Start()
    {
        animator = GetComponent<Animator>();//獲取動畫
        target = GameObject.FindWithTag("player").transform;//獲取角色位置
        navMeshAgent = GetComponent<NavMeshAgent>();//獲取尋路插件
        navMeshAgent.speed = MoveSpeed;//設定尋路器的行走速度
        if (navMeshAgent==null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
    }


    void Update()
    {
        navMeshAgent.SetDestination(target.transform.position); //設置尋路目標
        Vector2 input = target.transform.position;
        if (input != Vector2.zero)
        {
            animator.SetBool("AIismove", true);
        }
        else
        {
            animator.SetBool("AIismove", false);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "wuqi")
        {
            GameObject.Find("huojuling").GetComponent<AIshuxing>().TakeDamage(guaiwugongjili);//(Clone)
        }
        if (collision.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().TakeDamage(guaiwugongjili);
        }
    }
}

internal class AIshuxing
{
    internal void TakeDamage(object guaiwugongjili)
    {
        throw new NotImplementedException();
    }
}