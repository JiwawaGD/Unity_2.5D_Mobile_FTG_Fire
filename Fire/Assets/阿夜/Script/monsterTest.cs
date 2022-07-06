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
    //�n�������ؼ�
    public Transform Target;

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
        //�p�⪱�a�P�ĤH���Z��
        float distsance = Vector3.Distance(transform.position, Target.position);
        //���a�P�ĤH����V�V�q
        Vector3 temVec = Target.position - transform.position;
        //�P���a���e�谵�I�n
        float forwardDistance = Vector3.Dot(temVec, transform.forward.normalized);
        if (forwardDistance > 0 && forwardDistance <= 10)
        {
            float rightDistance = Vector3.Dot(temVec, transform.right.normalized);
            if (Mathf.Abs(rightDistance) <= 3)
            {
                Debug.Log("�i�J�����d��");
            }
        }
    }

    void FixedUpdate()
    {
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
    ////���a�i�J������d���,�p�Ƕi�����
    //public void OnTriggerEnter(Collision col)
    //{
    //    if (col.gameObject.tag == "Player" )
    //    {
    //        ani.SetBool("attacks", true);
    //    }
    //}
    ////���a���}��,�������
    //public void OnTriggerExit(Collision col)
    //{
    //    if (col.gameObject.tag == "Player")
    //    {
    //        ani.SetBool("attacks", false);
    //    }
    //}
}

