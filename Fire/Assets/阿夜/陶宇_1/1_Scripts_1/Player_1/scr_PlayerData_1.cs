using UnityEngine;

[CreateAssetMenu(fileName = "player_1", menuName = "Players's data_1")]
public class scr_PlayerData_1 : ScriptableObject
{
    [Header("角色名稱")] public string charactorName;

    [Header("移動速度")] public float moveSpeed;
    [Header("跳躍力度")] public float jumpForce;
    [Header("跳躍高度")] public float jumpHeight;

    [Header("生命值")] public float hp;
    [Header("魔力值")] public float mp;
    [Header("攻擊力")] public float atk;
    [Header("防禦力")] public float def;
}
