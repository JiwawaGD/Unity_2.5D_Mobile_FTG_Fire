using UnityEngine;

[CreateAssetMenu(fileName = "player", menuName = "Players's data")]
public class scr_PlayerData : ScriptableObject
{
    [Header("角色名稱")] public string charactorName;

    [Header("移動速度")] public float moveSpeed;
    [Header("跳躍力度")] public float jumpForce;
    [Header("跳躍高度")] public float jumpHeight;

    [Header("生命值")] public int hp;
    [Header("魔力值")] public float mp;
    [Header("攻擊力")] public float atk;
    [Header("護甲值")] public int armor;
}
