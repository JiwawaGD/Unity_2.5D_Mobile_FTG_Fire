using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

/// <summary>
/// �p�¬��ͦ�
/// </summary>
public class mobsMove : MonoBehaviour
{
    //�n�ͦ����p�ǡi �p�¬� �j
    public GameObject Mobs;
    //�p�ǥͦ��`�ƶq
    public int mobsNum = 10;
    //�ͦ��Ǫ����ɶ����j
    public float mobsTime = 3f;
    //�ͦ��Ǫ��p�ɾ�
    public int mobsCounter;
    //���a
    private GameObject player;



    void Start()
    {
        //�ͦ��Ǫ��p�ɾ�
        //player = GetComponent<GameObject>();
        //player = GameObject.Find("���a");
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
