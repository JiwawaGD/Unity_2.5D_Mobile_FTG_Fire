using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGwawaA : MonoBehaviour
{

    /// <summary>
    /// 選擇遊戲及娃無娃動畫
    /// </summary>
    public void StartGwawaM()
    {
        //移動位置(二為向量(70.15),分鐘).角色移動速度類型.循環播放();
        transform.LeanMoveLocal(new Vector2(70, 10), .4f).setEaseOutQuart().setLoopPingPong();
    }
}
