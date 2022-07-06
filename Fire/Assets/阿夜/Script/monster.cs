using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 小怪
/// </summary>
public class monster : MonoBehaviour
{
    //要攻擊的目標
    public Transform Target;
    //攻擊動畫
    private Animation ani;

    private void Start()
    {
        ani = GetComponent<Animation>();
    }

    private void Update()
    {
        //計算玩家與敵人的距離
        float distance = Vector3.Distance(transform.position, Target.position);
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
}