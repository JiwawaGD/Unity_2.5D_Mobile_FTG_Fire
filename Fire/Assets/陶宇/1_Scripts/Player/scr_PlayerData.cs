using UnityEngine;

[CreateAssetMenu(fileName = "player", menuName = "Players's data")]
public class scr_PlayerData : ScriptableObject
{
    [Header("����W��")] public string charactorName;

    [Header("���ʳt��")] public float moveSpeed;
    [Header("���D�O��")] public float jumpForce;
    [Header("���D����")] public float jumpHeight;

    [Header("�ͩR��")] public int hp;
    [Header("�]�O��")] public float mp;
    [Header("�����O")] public float atk;
    [Header("�@�ҭ�")] public int armor;
}
