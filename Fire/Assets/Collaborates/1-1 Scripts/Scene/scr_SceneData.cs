using UnityEngine;

[CreateAssetMenu(fileName = "Scene", menuName = "Scene/Scene's data")]
public class scr_SceneData : ScriptableObject
{
    [Header("���a���ʥ��k���")] public Vector2 playerxLimit;
    [Header("��v�����k���")] public Vector2 cameraxLimit;

    [Header("Enemies GameObjects")] public GameObject[] enemies_Obj;
    [Header("Spawn Enemy Type")] public SpawnType spawnType;
    [Header("Spawn Enemy Bools")] public bool[] checkBools;
    [Header("Spawn Enemy Points")] public GameObject[] checkPoints;
    [Header("CouldSpawn")] public bool couldSpawn;
    [Header("Have Boss")] public bool haveBoss;
    [Header("�q���Ǫ��ƶq")] public int passAmount;

    public scr_SceneData()
    {
        couldSpawn = false;
    }
}
