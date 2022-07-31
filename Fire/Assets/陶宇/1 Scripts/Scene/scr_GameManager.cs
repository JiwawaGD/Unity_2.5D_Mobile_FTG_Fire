using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_GameManager : MonoBehaviour
{
    #region - Fields -
    [Header("場景資料")] public scr_SceneData sceneData;
    [SerializeField] [Header("傳送門")] GameObject portal;

    [Header("玩家移動左右邊界")] public Vector2 playerxLimit;
    [Header("攝影機左右邊界")] public Vector2 cameraLimit;

    [SerializeField] [Header("任務是否通關")] bool isPass;
    [SerializeField] [Header("是否已經生過怪物")] bool hasSpawn;
    [SerializeField] [Header("是否可以生過怪物")] bool couldSpawn;
    [SerializeField] [Header("關卡生怪方法")] SpawnType spawnType;

    [Header("按著往左")] public bool holding_left;
    [Header("按著往右")] public bool holding_Right;
    [Header("按著防禦")] public bool holding_Defense;

    int passAmount; // 關卡需要擊殺小怪數量
    int killAmount; // 玩家關卡擊殺小怪數量

    GameObject player;

    #endregion

    #region - MonoBehaviour -
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("玩家");
    }

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
        cameraLimit = sceneData.cameraxLimit;
        passAmount = sceneData.enemyAmount;

        killAmount = 0;

        isPass = false;
        hasSpawn = false;
    }

    /// <summary>
    /// 生成敵人
    /// </summary>
    void SpawnEnemy()
    {
        if (isPass) return;

        switch (spawnType)
        {
            case SpawnType.Already:
                // unnecessary to do anythins
                break;

            case SpawnType.EnterZone:
                /*
                if (!hasSpawn)
                {
                    hasSpawn = !hasSpawn;

                    if (player.transform.position.x >= 100f)
                    {
                        int amount = 10;
                        for (int i = 0; i < amount; i++) Initialize(enemy, point, angle);
                    }
                }*/
                break;

            case SpawnType.Continous:
                /*
                if (player.transform.position.x >= 30f) couldSpawn = !couldSpawn;

                if (couldSpawn) InvokeRepeating("SpawnLoop", 0.5f, 1.5f);*/
                break;
        }
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

    /// <summary>
    /// 循環生怪
    /// </summary>
    void SpawnLoop()
    {

    }
    #endregion
}

/// <summary>
/// 怪物生成模式
/// </summary>
public enum SpawnType
{
    Already,
    EnterZone,
    Continous
}
