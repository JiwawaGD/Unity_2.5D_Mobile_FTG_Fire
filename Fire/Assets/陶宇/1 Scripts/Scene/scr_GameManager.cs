using UnityEngine;
using UnityEngine.SceneManagement;

public class scr_GameManager : MonoBehaviour
{
    #region - Fields -
    [SerializeField] [Header("�������")] scr_SceneData sceneData;
    [SerializeField] [Header("�ǰe��")] GameObject portal;

    [Header("���a���ʥ��k���")] public Vector2 playerxLimit;
    [Header("��v�����k���")] public Vector2 cameraxLimit;

    [SerializeField] [Header("���ȬO�_�q��")] bool isPass;

    [Header("���۩���")] public bool holding_left;
    [Header("���۩��k")] public bool holding_Right;
    [Header("���ۨ��m")] public bool holding_Defense;

    int passAmount; // ���d�ݭn�����p�Ǽƶq
    int killAmount; // ���a���d�����p�Ǽƶq

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
        cameraxLimit = sceneData.cameraxLimit;
        passAmount = sceneData.enemyAmount;

        killAmount = 0;

        isPass = false;
    }

    /// <summary>
    /// �ͦ��ĤH
    /// </summary>
    void SpawnEnemy()
    {
        if (isPass) return;
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
    #endregion
}
