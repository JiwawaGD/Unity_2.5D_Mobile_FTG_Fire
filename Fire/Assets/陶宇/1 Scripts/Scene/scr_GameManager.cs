using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_GameManager : MonoBehaviour
{
    #region - Variables -
    [Header("場景資料")] public scr_SceneData sceneData;
    [SerializeField] [Header("傳送門")] GameObject portal;

    // Limit
    [Header("玩家移動左右邊界")] public Vector2 playerxLimit;
    [Header("攝影機左右邊界")] public Vector2 cameraLimit;

    // 關卡 怪物/通關 判斷
    int passAmount;             // 關卡需要擊殺小怪數量
    int killAmount;             // 玩家關卡擊殺小怪數量
    float spawnTimer;           // 生成計時器
    bool isPass;                // 關卡是否通關
    bool haveBoss;              // 該關卡是否有王
    bool couldSpawn;            // 是否可以生成怪物
    bool[] checkBools;          // 確認是否生怪
    GameObject[] checkPoints;   // 關卡生怪確認點
    Transform enemyParent;      // 敵人物件母座標
    SpawnType spawnType;        // 關卡生怪方法
    GameObject[] enemies_Obj;   // 關卡敵人種類
    GameObject[] enemyCount;    // 持續更新 關卡敵人數量

    // Event Trigger
    [HideInInspector] public bool holding_left;      // 按著往左
    [HideInInspector] public bool holding_Right;     // 按著往右
    [HideInInspector] public bool holding_Defense;   // 按著防禦

    [SerializeField] [Header("普攻 icon")] Image attack_icon;
    [SerializeField] [Header("技能 icon")] Image[] skill_icon = new Image[3];
    [SerializeField] [Header("HUD 背景")] Sprite[] hud_sprites;
    Image hud_bg;

    GameObject player;
    scr_PlayerData playerData;

    #endregion

    #region - MonoBehaviour -
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("玩家");
        playerData = player.GetComponent<scr_PlayerBase>().playerdata;

        attack_icon = GameObject.Find("Canvas/Basic/Buttons/攻 - Btn").GetComponent<Image>();
        skill_icon[0] = GameObject.Find("Canvas/Basic/Buttons/技能 1 - Btn").GetComponent<Image>();
        skill_icon[1] = GameObject.Find("Canvas/Basic/Buttons/技能 2 - Btn").GetComponent<Image>();
        skill_icon[2] = GameObject.Find("Canvas/Basic/Buttons/技能 3 - Btn").GetComponent<Image>();
        hud_bg = GameObject.Find("Canvas/HUD/bg").GetComponent<Image>();

        enemyParent = GameObject.Find(" - Enemies - ").GetComponent<Transform>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        SpawnEnemy();
        ActivatePortal();

        spawnTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Tab)) UpdateEnemyCount();
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
    /// 更新過關資訊
    /// </summary>
    public void UpdateEnemyCount()
    {
        killAmount++;

        if (haveBoss) return;

        if (killAmount >= passAmount) isPass = true;
    }

    /// <summary>
    /// 更新玩家 HUD
    /// </summary>
    void UpdateHUD()
    {

    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Init()
    {
        holding_left = false;
        holding_Right = false;
        holding_Defense = false;

        playerxLimit = sceneData.playerxLimit;
        cameraLimit = sceneData.cameraxLimit;

        passAmount = sceneData.passAmount;
        killAmount = 0;

        spawnType = sceneData.spawnType;
        checkBools = sceneData.checkBools;
        checkPoints = sceneData.checkPoints;
        enemies_Obj = sceneData.enemies_Obj;
        couldSpawn = sceneData.couldSpawn;
        haveBoss = sceneData.haveBoss;

        isPass = false;

        if (player.name.Contains("吉娃娃"))
        {
            hud_bg.sprite = hud_sprites[0];
            attack_icon.sprite = playerData.icon;
        }
        if (player.name.Contains("鵝")) 
        { 
            hud_bg.sprite = hud_sprites[1];
        }
    }

    /// <summary>
    /// 開啟傳送門
    /// </summary>
    void ActivatePortal()
    {
        portal.SetActive(isPass);
    }

    /// <summary>
    /// 生成敵人
    /// </summary>
    void SpawnEnemy()
    {
        if (isPass) return;

        enemyCount = GameObject.FindGameObjectsWithTag("Enemy");

        switch (spawnType)
        {
            case SpawnType.Already:
                // unnecessary to do anythins
                break;

            case SpawnType.EnterZone:
                for (int i = 0; i < checkBools.Length; i++)
                {
                    if (player.transform.position.x >= checkPoints[i].transform.position.x && !checkBools[i])
                    {
                        checkBools[i] = true;

                        GameObject temp01 = Instantiate(enemies_Obj[0]);
                        GameObject temp02 = Instantiate(enemies_Obj[1]);

                        temp01.transform.SetParent(enemyParent);
                        temp02.transform.SetParent(enemyParent);

                        temp01.transform.position = new Vector3(checkPoints[i].transform.position.x - 5f, checkPoints[i].transform.position.y, checkPoints[i].transform.position.z);
                        temp02.transform.position = new Vector3(checkPoints[i].transform.position.x + 3f, checkPoints[i].transform.position.y + 3f, checkPoints[i].transform.position.z);
                    }
                }
                break;

            case SpawnType.Continous:
                if (player.transform.position.x >= checkPoints[0].transform.position.x) couldSpawn = true;
                SpawnLoop(couldSpawn);
                break;
        }
    }

    /// <summary>
    /// 循環生怪
    /// </summary>
    void SpawnLoop(bool _couldSpawn)
    {
        if (enemyCount.Length >= 8) return;

        if (couldSpawn && spawnTimer >= 0.8f)
        {
            GameObject temp = Instantiate(enemies_Obj[0]);
            temp.transform.SetParent(enemyParent);
            temp.transform.position = checkPoints[0].transform.position;

            spawnTimer = 0;
        }
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
