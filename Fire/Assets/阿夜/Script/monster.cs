using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

/// <summary>
/// �p��
/// </summary>
public class monster : MonoBehaviour
{
    private Transform target;//�]�m�l�ܥؼЪ���m
    public float MoveSpeed = 2.5f;//�ĤH���ʳt��
    private NavMeshAgent navMeshAgent;//�]�m�M���ե�
    private Animator animator;//�w�q�ܶq�ʵe
    public float guaiwugogjili;
    

    void Start()
    {
        animator = GetComponent<Animator>();//����ʵe
        target = GameObject.FindWithTag("player").transform;//��������m
        navMeshAgent = GetComponent<NavMeshAgent>();//����M������
        navMeshAgent.speed = MoveSpeed;//�]�w�M�������樫�t��
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
    }


    void Update()
    {
        navMeshAgent.SetDestination(target.transform.position); //�]�m�M���ؼ�
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
}