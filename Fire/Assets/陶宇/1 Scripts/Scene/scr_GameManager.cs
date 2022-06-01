using UnityEngine;

public class scr_GameManager : MonoBehaviour
{
    [SerializeField] [Header("場景資料")] scr_SceneData sceneData;

    [Header("玩家移動左右邊界")] public Vector2 playerxLimit;
    [Header("攝影機左右邊界")] public Vector2 cameraxLimit;

    void Start()
    {
        Initialize();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        playerxLimit = sceneData.playerxLimit;
        cameraxLimit = sceneData.cameraxLimit;
    }
}
