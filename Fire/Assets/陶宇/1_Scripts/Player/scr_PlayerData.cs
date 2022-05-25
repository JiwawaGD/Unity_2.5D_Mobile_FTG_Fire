using UnityEngine;

[CreateAssetMenu(fileName = "Charactor", menuName = "Charactor's data")]
public class scr_PlayerData : ScriptableObject
{
    [Header("����W��")] public string CharactorName;

    [Header("�ͩR��")] public float hp;
    [Header("����")] public float rage;
    [Header("�@�ҭ�")] public float armor;
    [Header("�����O")] public float atk;

    [Header("�ޯ��")] public PlayerSkill[] playerSkills;
}

[System.Serializable]
public class PlayerSkill
{
    [Header("�ޯ�W��")] public string name;
    [Header("�ޯ�ʵe�ɪ�")] public float time;
    [Header("�ޯ�ˮ`")] public float damage;
    [Header("�ޯ����")] public int cost;
}