using UnityEngine;

[CreateAssetMenu(fileName = "Charactor", menuName = "Charactor's data")]
public class scr_PlayerData : ScriptableObject
{
    [Header("角色名稱")] public string CharactorName;

    [Header("生命值")] public float hp;
    [Header("怒氣值")] public float rage;
    [Header("護甲值")] public float armor;
    [Header("攻擊力")] public float atk;
    [Header("攻擊動畫時間")] public float[] attackTime = new float[3];

    [Header("技能組")] public PlayerSkill[] playerSkills = new PlayerSkill[3];
}

[System.Serializable]
public class PlayerSkill
{
    [Header("技能名稱")] public string name;
    [Header("技能動畫時長")] public float time;
    [Header("技能傷害")] public float damage;
    [Header("技能消耗")] public int cost;
}