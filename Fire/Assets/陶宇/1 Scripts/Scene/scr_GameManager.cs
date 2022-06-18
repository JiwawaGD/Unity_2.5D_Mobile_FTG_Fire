using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_GameManager : MonoBehaviour
{
    #region - Fields -
    [SerializeField] [Header("場景資料")] scr_SceneData sceneData;
    [SerializeField] [Header("傳送門")] GameObject portal;

    [Header("玩家移動左右邊界")] public Vector2 playerxLimit;
    [Header("攝影機左右邊界")] public Vector2 cameraxLimit;

    [SerializeField] [Header("任務是否通關")] bool isPass;

    int passAmount; // 關卡需要擊殺小怪數量
    int killAmount; // 玩家關卡擊殺小怪數量
    #endregion

    #region - MonoBehaviour -
    void Start()
    {
        Initialize();
    }

    void Update()
    {
        PassLevel();
        ActivatePortal();
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// 選擇關卡
    /// </summary>
    public static void ChooseLevel(int _sceneBuildIndex)
    {
        SceneManager.LoadScene(_sceneBuildIndex);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        playerxLimit = sceneData.playerxLimit;
        cameraxLimit = sceneData.cameraxLimit;
        passAmount = sceneData.enemyAmount;

        killAmount = 0;

        isPass = false;
    }

    /// <summary>
    /// 生成敵人
    /// </summary>
    void SpawnEnemy()
    {
        if (isPass) return;
    }

    /// <summary>
    /// 通關確認
    /// </summary>
    void PassLevel()
    {
        if (killAmount == passAmount) isPass = true;
    }

    /// <summary>
    /// 開啟傳送門
    /// </summary>
    void ActivatePortal()
    {
        portal.SetActive(isPass);
    }
    #endregion
}
