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

    int passAmount; // ���d�ݭn�����p�Ǽƶq
    int killAmount; // ���a���d�����p�Ǽƶq
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
