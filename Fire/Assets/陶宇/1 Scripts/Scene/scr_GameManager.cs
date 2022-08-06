using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scr_GameManager : MonoBehaviour
{
    #region - Variables -
    [Header("�������")] public scr_SceneData sceneData;
    [SerializeField] [Header("�ǰe��")] GameObject portal;

    // Limit
    [Header("���a���ʥ��k���")] public Vector2 playerxLimit;
    [Header("��v�����k���")] public Vector2 cameraLimit;

    // ���d �Ǫ�/�q�� �P�_
    int passAmount;             // ���d�ݭn�����p�Ǽƶq
    int killAmount;             // ���a���d�����p�Ǽƶq
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

    Image hud_bg;
    [SerializeField] [Header("HUD �I��")] Sprite[] hud_sprites;
    GameObject player;

    #endregion

    #region - MonoBehaviour -
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("���a");
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
        killAmount++;

        if (haveBoss) return;

        if (killAmount >= passAmount) isPass = true;
    }

    /// <summary>
    /// ��s���a HUD
    /// </summary>
    void UpdateHUD()
    {

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

        if (player.name.Contains("�N����")) hud_bg.sprite = hud_sprites[0];
        if (player.name.Contains("�Z")) hud_bg.sprite = hud_sprites[1];
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
