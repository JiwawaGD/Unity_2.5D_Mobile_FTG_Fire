using UnityEngine;

[CreateAssetMenu(fileName = "player_1", menuName = "Players's data_1")]
public class scr_PlayerData_1 : ScriptableObject
{
    [Header("����W��")] public string charactorName;

    [Header("���ʳt��")] public float moveSpeed;
    [Header("���D�O��")] public float jumpForce;
    [Header("���D����")] public float jumpHeight;

    [Header("�ͩR��")] public float hp;
    [Header("�]�O��")] public float mp;
    [Header("�����O")] public float atk;
    [Header("���m�O")] public float def;
}
