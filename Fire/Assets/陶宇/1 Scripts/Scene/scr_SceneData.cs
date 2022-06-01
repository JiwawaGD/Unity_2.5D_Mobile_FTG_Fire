using UnityEngine;

[CreateAssetMenu(fileName = "Scene", menuName = "Scene's data")]
public class scr_SceneData : ScriptableObject
{
    [Header("玩家移動左右邊界")] public Vector2 playerxLimit;
    [Header("攝影機左右邊界")] public Vector2 cameraxLimit;
}
