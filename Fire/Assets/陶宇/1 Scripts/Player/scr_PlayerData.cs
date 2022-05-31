using UnityEngine;

[CreateAssetMenu(fileName = "Charactor", menuName = "Charactor's data")]
public class scr_PlayerData : ScriptableObject
{
    [Header("����W��")] public string CharactorName;

    [Header("�ͩR��")] public float hp;
    [Header("����")] public float rage;
    [Header("�@�ҭ�")] public float armor;
    [Header("�����O")] public float atk;
    [Header("�����ʵe�ɶ�")] public float[] attackTime = new float[3];

    [Header("�ޯ��")] public PlayerSkill[] playerSkills = new PlayerSkill[3];
}

[System.Serializable]
public class PlayerSkill
{
    [Header("�ޯ�W��")] public string name;
    [Header("�ޯ�ʵe�ɪ�")] public float time;
    [Header("�ޯ�ˮ`")] public float damage;
    [Header("�ޯ����")] public int cost;
}