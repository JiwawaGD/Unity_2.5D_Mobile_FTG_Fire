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

    [Header("按著往左")] public bool holding_left;
    [Header("按著往右")] public bool holding_Right;
    [Header("按著防禦")] public bool holding_Defense;

    int passAmount; // 關卡需要擊殺小怪數量
    int killAmount; // 玩家關卡擊殺小怪數量

    scr_PlayerBase playerBase;
    #endregion

    #region - MonoBehaviour -
    void Start()
    {
        playerBase = FindObjectOfType<scr_PlayerBase>();

        Initialize();
    }

    void Update()
    {
        PassLevel();
        ActivatePortal();
    }
    #endregion

    #region - Event Trigger -
    /// <summary>
    /// 按著左鍵
    /// </summary>
    /// <param name="press">是否按著</param>
    public void HoldLeft(bool press)
    {
        holding_left = press;
    }

    /// <summary>
    /// 按著右鍵
    /// </summary>
    /// <param name="press">是否按著</param>
    public void HoldRight(bool press)
    {
        holding_Right = press;
    }

    /// <summary>
    /// 按著防禦
    /// </summary>
    /// <param name="press">是否按著</param>
    public void HoldDefense(bool press)
    {
        holding_Defense = press;
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
        holding_left = false;
        holding_Right = false;
        holding_Defense = false;

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
