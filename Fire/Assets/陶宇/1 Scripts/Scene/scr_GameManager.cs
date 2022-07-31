using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_GameManager : MonoBehaviour
{
    #region - Fields -
    [Header("�������")] public scr_SceneData sceneData;
    [SerializeField] [Header("�ǰe��")] GameObject portal;

    [Header("���a���ʥ��k���")] public Vector2 playerxLimit;
    [Header("��v�����k���")] public Vector2 cameraLimit;

    [SerializeField] [Header("���ȬO�_�q��")] bool isPass;
    [SerializeField] [Header("�O�_�w�g�͹L�Ǫ�")] bool hasSpawn;
    [SerializeField] [Header("�O�_�i�H�͹L�Ǫ�")] bool couldSpawn;
    [SerializeField] [Header("���d�ͩǤ�k")] SpawnType spawnType;

    [Header("���۩���")] public bool holding_left;
    [Header("���۩��k")] public bool holding_Right;
    [Header("���ۨ��m")] public bool holding_Defense;

    int passAmount; // ���d�ݭn�����p�Ǽƶq
    int killAmount; // ���a���d�����p�Ǽƶq

    GameObject player;

    #endregion

    #region - MonoBehaviour -
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("���a");
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
    /// ��l��
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
    /// �ͦ��ĤH
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
    /// �q���T�{
    /// </summary>
    void PassLevel()
    {
        if (killAmount == passAmount) isPass = true;
    }

    /// <summary>
    /// �}�Ҷǰe��
    /// </summary>
    void ActivatePortal()
    {
        portal.SetActive(isPass);
    }

    /// <summary>
    /// �`���ͩ�
    /// </summary>
    void SpawnLoop()
    {

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
