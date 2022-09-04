using UnityEngine;

[CreateAssetMenu(fileName = "Scene", menuName = "Scene/Scene's data")]
public class scr_SceneData : ScriptableObject
{
    [Header("玩家移動左右邊界")] public Vector2 playerxLimit;
    [Header("攝影機左右邊界")] public Vector2 cameraxLimit;

    [Header("Enemies GameObjects")] public GameObject[] enemies_Obj;
    [Header("Spawn Enemy Type")] public SpawnType spawnType;
    [Header("Spawn Enemy Bools")] public bool[] checkBools;
    [Header("Spawn Enemy Points")] public GameObject[] checkPoints;
    [Header("CouldSpawn")] public bool couldSpawn;
    [Header("Have Boss")] public bool haveBoss;
    [Header("通關怪物數量")] public int passAmount;

    public scr_SceneData()
    {
        couldSpawn = false;
    }
}
