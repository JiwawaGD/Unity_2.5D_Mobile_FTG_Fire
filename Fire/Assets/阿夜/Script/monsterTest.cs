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
    //�C���������ɶ�
    private float attTime;
    //�p�����������
    private int attNum = 0;
    //�p��ޯ�I�񦸼�
    private int skillNum;
    //�p�⪱�a�I�쪺����
    //private int hurtNum;

    //�ͩR��
    private float HP = 100;

    //�ʵe
    Animator ani;
    ////�ʵe�ɪ�
    //public AnimatorClipInfo attInfo;
    //��m
    Vector3 pos;
    //�n�������ؼ�
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
        //����
        if (Mathf.Abs(distsance) == 0)
        {
            float hurtNum = HP - 10;
            ani.SetTrigger("hurt");
            //���`
            if (hurtNum ==0)
            {
                ani.SetBool("dead",true);
                Destroy(this.gameObject);
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



    /// <summary>
    /// ������k
    /// </summary>
    void Attack()
    {
        skillNum++;
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
            ani.SetTrigger("attack");
        }
    }

    ///// <summary>
    ///// ����
    ///// </summary>
    //void hurt()
    //{
    //    float distsance = vector3.distance(transform.position, target.position);
    //    if (distsance <= 0)
    //    {
    //        float hurtnum = hp - 10;
    //        ani.settrigger("hurt");
    //    }
    //}

    ///// <summary>
    ///// ���`
    ///// </summary>
    //void dead()
    //{
    //    ani.setbool("dead", true);
    //}
    ///// <summary>
    ///// �I��ޯ�
    ///// </summary>
    //void Skill()
    //{
    //    if (skillNum >= 5)
    //    {
    //        ani.SetTrigger("skill");
    //        skillNum = 0;
    //        timeMove = 0;
    //        canMove = false;
    //        ani.SetBool("walk", false);
    //    }
    //}
}

