using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_GameManager : MonoBehaviour
{
    #region - Variables -
    [Header("�������")] public scr_SceneData sceneData;
    [SerializeField] [Header("�ǰe��")] GameObject portal;
    [SerializeField] [Header("���a�ͦ��I")] GameObject playerSpawnPoint;

    // Limit
    [Header("���a���ʥ��k���")] public Vector2 playerxLimit;
    [Header("��v�����k���")] public Vector2 cameraLimit;

    // ���d �Ǫ�/�q�� �P�_
    int passAmount;             // ���d�ݭn�����p�Ǽƶq
    float spawnTimer;           // �ͦ��p�ɾ�
    bool isPass;                // ���d�O�_�q��
    bool haveBoss;              // �����d�O�_����
    bool couldSpawn;            // �O�_�i�H�ͦ��Ǫ�
    bool[] checkBools;          // �T�{�O�_�ͩ�
    GameObject[] checkPoints;   // ���d�ͩǽT�{�I
    Transform enemyParent;      // �ĤH������y��
    SpawnType spawnType;        // ���d�ͩǤ�k
    GameObject[] enemies_Obj;   // ���d�ĤH����
    GameObject[] enemyCount;    // �����s ���d�ĤH�ƶq

    // Event Trigger
    [HideInInspector] public bool holding_left;      // ���۩���
    [HideInInspector] public bool holding_Right;     // ���۩��k
    [HideInInspector] public bool holding_Defense;   // ���ۨ��m

    [HideInInspector] public int killAmount;         // ���a���d�����p�Ǽƶq

    Image[] skill_icon = new Image[3];               // �ޯ� icon
    Image attack_icon;                               // ���� icon
    Image hud_bg;

    GameObject player;
    scr_PlayerData playerData;
    #endregion

    #region - MonoBehaviour -
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("���a");
        playerData = player.GetComponent<scr_PlayerBase>().playerdata;

        attack_icon = GameObject.Find("Canvas/Basic/Buttons/�� - Btn").GetComponent<Image>();
        skill_icon[0] = GameObject.Find("Canvas/Basic/Buttons/�ޯ� 1 - Btn").GetComponent<Image>();
        skill_icon[1] = GameObject.Find("Canvas/Basic/Buttons/�ޯ� 2 - Btn").GetComponent<Image>();
        skill_icon[2] = GameObject.Find("Canvas/Basic/Buttons/�ޯ� 3 - Btn").GetComponent<Image>();
        hud_bg = GameObject.Find("Canvas/HUD/bg").GetComponent<Image>();

        enemyParent = GameObject.Find(" - Enemies - ").GetComponent<Transform>();
    }

    void Start()
    {
        Init();
        SpawnPlayer();
    }

    void Update()
    {
        SpawnEnemy();
        ActivatePortal();
        UpdateEnemyCount();
        Timer();
    }

    void Timer()
    {
        if (spawnTimer < 2f) spawnTimer += Time.deltaTime;
    }
    #endregion

    #region - Event Trigger -
    /// <summary>
    /// ���ۥ���
    /// </summary>
    /// <param name="press">�O�_����</param>
    public void HoldLeft(bool press)
    {
        holding_left = press;
    }

    /// <summary>
    /// ���ۥk��
    /// </summary>
    /// <param name="press">�O�_����</param>
    public void HoldRight(bool press)
    {
        holding_Right = press;
    }

    /// <summary>
    /// ���ۨ��m
    /// </summary>
    /// <param name="press">�O�_����</param>
    public void HoldDefense(bool press)
    {
        holding_Defense = press;
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// ������d
    /// </summary>
    public static void ChooseLevel(int _sceneBuildIndex)
    {
        SceneManager.LoadScene(_sceneBuildIndex);
    }

    /// <summary>
    /// ��s�L����T
    /// </summary>
    public void UpdateEnemyCount()
    {
        if (haveBoss) return;

        if (killAmount >= passAmount) isPass = true;
    }

    /// <summary>
    /// ��l��
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

        hud_bg.sprite = playerData.hud_icon;
        attack_icon.sprite = playerData.attack_icon;

        skill_icon[0].sprite = playerData.playerSkills[0].icon;
        skill_icon[1].sprite = playerData.playerSkills[1].icon;
        skill_icon[2].sprite = playerData.playerSkills[2].icon;

        Physics.IgnoreLayerCollision(6, 6);
        Physics.IgnoreLayerCollision(10, 10);
        Physics.IgnoreLayerCollision(10, 11);
    }

    /// <summary>
    /// �ͦ����a
    /// </summary>
    void SpawnPlayer()
    {
        Instantiate(StaticField.player, playerSpawnPoint.transform);
    }

    void PlayerDead()
    { 
    
    }

    /// <summary>
    /// �}�Ҷǰe��
    /// </summary>
    void ActivatePortal()
    {
        portal.SetActive(isPass);
    }

    /// <summary>
    /// �ͦ��ĤH
    /// </summary>
    void SpawnEnemy()
    {
        if (isPass) return;

        enemyCount = GameObject.FindGameObjectsWithTag("EnemyBodyCollider");

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
    /// �`���ͩ�
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
/// �Ǫ��ͦ��Ҧ�
/// </summary>
public enum SpawnType
{
    Already,
    EnterZone,
    Continous
}
