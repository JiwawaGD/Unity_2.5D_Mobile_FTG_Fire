using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCreater : MonoBehaviour
{
    //�n�Q�ͦ����Ǫ�����
    public GameObject Monster;
    //�ͦ��Ǫ��̤j�ƶq
    private int monsterMax = 10;

    void Start()
    {
        //����ͦ��Ǫ��{���X(�C��@��)
        InvokeRepeating("CreatMoneter", 3, 2);
    }

    public void CreatMoneter()
    {
        int MonsterNum;
        //�H���M�w�n�ͦ��X�өǪ�(0-2���H��)
        MonsterNum = Random.Range(0, 2);
        //�}�l�ͦ��Ǫ�
        for (int i = 0; i < MonsterNum; i++)
        {
            //�ŧi�ͦ���X�y��
            float x;
            //�����H����X�y��(-6��5����)
            x = Random.Range(0, 18);
            //�ͦ��Ǫ�
            Instantiate(Monster, new Vector3(x, -4f, 0), Quaternion.identity);
            //if (MonsterNum >= monsterMax)
            //{

            //}
        }
    }
}

