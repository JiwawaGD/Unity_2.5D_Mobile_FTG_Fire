using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGwawaA : MonoBehaviour
{

    /// <summary>
    /// ��ܹC���Ϋ��L���ʵe
    /// </summary>
    public void StartGwawaM()
    {
        //���ʦ�m(�G���V�q(70.15),����).���Ⲿ�ʳt������.�`������();
        transform.LeanMoveLocal(new Vector2(70, 10), .4f).setEaseOutQuart().setLoopPingPong();
    }
}
