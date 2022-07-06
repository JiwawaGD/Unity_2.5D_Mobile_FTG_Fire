using UnityEngine;

[CreateAssetMenu(fileName = "Scene", menuName = "Scene/Scene's data")]
public class scr_SceneData : ScriptableObject
{
    [Header("���a���ʥ��k���")] public Vector2 playerxLimit;
    [Header("��v�����k���")] public Vector2 cameraxLimit;

    [Header("���d�Ǫ��ƶq")] public int enemyAmount;
}
