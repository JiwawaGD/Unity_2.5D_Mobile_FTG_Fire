using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creatBlack : MonoBehaviour
{
    //�n�ͦ����Ǫ�����
    public GameObject Black;

    void Start()
    {
        //����ͦ��Ǫ��{���X(�C��@��)
        InvokeRepeating("CreatBlack", 1, 1);
    }

    public void CreatMoneter()
    {
        int MonsterNum;
        //�H���M�w�n�ͦ��X�өǪ�(0-2���H��)
        MonsterNum = Random.Range(0, 3);
        //�}�l�ͦ��Ǫ�
        for (int i = 0; i < MonsterNum; i++)
        {
            //�ŧi�ͦ���X�y��
            float x;
            //�����H����X�y��(-6��5����)
            x = Random.Range(-6, 6);
            //�ͦ��Ǫ�
            Instantiate(Black, new Vector3(x, 2.8f, 0), Quaternion.identity);
        }
    }
}
