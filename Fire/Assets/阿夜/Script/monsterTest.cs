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
    private float timeMove;//�p�ɾ�
    
    void Start()
    {
       
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

                //�p�G�ڲ���F3 ���򱵤U�ӴN�O����,�ҥH��k���]��false
                if (transform.position.x >= 3)
                {
                    canMove = false;
                    timeMove = 0;
                    movingRight = false;
                }
            }
            else
            {
                //��ڦb�����A�ӥBx�b�y�Ш�F-3�A�������������A�}�l�k��
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);

                //���������A�k���}�l�A�]�m���x��ture
                if (transform.position.x <= -3)
                {
                    canMove = false;
                    timeMove = 0;
                    movingRight = true;
                }
            }
        }
        
    }
}
