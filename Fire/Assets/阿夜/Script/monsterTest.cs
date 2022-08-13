using System;
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
    [Header("�p�ɾ�")]
    private float timeMove;
    [Header("�C���������ɶ�")]
    private float attTime;
    [Header("�p�����������")]
    private int attNum = 0;
    [Header("�p��ޯ�I�񦸼�")]
    private int skillNum;
    //�p�⪱�a�I�쪺����
    //private int hurtNum;
    private float desTime = 1f;

    [Header("�ͩR��")]
    private float HP = 100;
   
    private float hurt ;

    [Header("�ʵe")]
    Animator ani;

    [Header("��m")]
    Vector3 pos;

    [Header("�n�������ؼ�")]
    public Transform Target;




    void Start()
    {
        pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        ani = GetComponent<Animator>();
        skillNum = 0;
        attTime = 0;
        //hurtNum = 0;

    }

    void Update()
    {
        timeMove += Time.deltaTime;
        attTime += Time.deltaTime;
        Move();
       
    }

    void FixedUpdate()
    {

    }

    /// <summary>
    /// ���ʤ�k
    /// </summary>
    void Move()
    {
        if (timeMove >= 4)
        {
            canMove = true;
        }
        //�p�⪱�a�P�ĤH���Z��
        float distsance = Vector3.Distance(transform.position, Target.position);
        //Debug.Log(distsance);

        if (Mathf.Abs(distsance) <= 3)
        {
            attNum++;
            canMove = false;
            timeMove = 0;
            if (attTime > 1.283f)
            {
                Attack();
            }
        }


        if (canMove)
        {
            //�bx�b-3��3�������k�`������
            if (movingRight)
            {
                //�k��
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
                ani.SetBool("walk", true);

                //�p�G����F3 ���򱵤U�ӴN�O����,��k���]��false
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
                //��ڦb�����A�ӥBx�b�y�Ш�F-3�A�������������A�}�l�k��
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);
                ani.SetBool("walk", true);

                //���������A�k���}�l�A�]�m���x��ture
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



    /// <summary> ������k</summary>
    void Attack()
    {
        skillNum++;
        Debug.Log("�ޯ�I��˼�" + " : " + skillNum);
        attTime = 0;
        if (skillNum >= 5)
        {
            ani.SetTrigger("skill");
            skillNum = 0;
        }
        else
        {
            timeMove = 0;
            canMove = false;
            ani.SetBool("walk", false);
            ani.SetTrigger("attacksss");
        }
    }


    /// <summary>���a�����p�� ----�p�Ǩ��� </summary>
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag =="Player")
        {
            Damage( );
        }
    }

    /// <summary>����</summary>
    void Damage()
    {
        HP += -10;
        if (HP <=0)
        {
            ani.SetBool("dead", true);
            Destroy(this.gameObject, desTime);
        }
        else
        {
            ani.SetTrigger("hurt");
            Debug.Log("����-10");
        }
    }
}

