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
    //���a��ĤH�������Z��
    float distance;

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
                    //if (pos)s
                    //{
                    //    if (Mathf.Abs(Transform.position.x - pos.position.x) < 10f)
                    //    {
                    //        ani.SetBool("walk", false);
                    //    }
                    //}
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
        ////��Z�� Ĳ�o �ĤH����
        //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

        ////�� �ۤv����m �� �ĤH����m
        //Vector2 toVector = transform.position - colliders[i].gameObject.transform.position;
        ////���o����
        //float nowAngle = Mathf.Atan2(toVector.y, toVector.x) * Mathf.Rad2Deg;
        ////��90
        //nowAngle = nowAngle - 90;
        ////���V��V
        //colliders[i].gameObject.transform.rotation = Quaternion.Euler(0, 0, nowAngle);
    }
}
