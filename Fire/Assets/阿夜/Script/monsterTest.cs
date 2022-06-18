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
    //�p�ɾ�
    private float timeMove;
    //�ʵe
    Animator ani;
    //��m
    Vector3 pos;

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
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            //���]�ڦbx�b-3��3�������k�`������
            if (movingRight)
            {
                //�k��
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
                ani.SetBool("walk", true);

                //�p�G�ڲ���F3 ���򱵤U�ӴN�O����,�ҥH��k���]��false
                if (transform.position.x - pos.x >= 5 )
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
}
