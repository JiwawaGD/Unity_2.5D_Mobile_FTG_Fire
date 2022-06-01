using UnityEngine;

public class scr_GameManager : MonoBehaviour
{
    [SerializeField] [Header("�������")] scr_SceneData sceneData;

    [Header("���a���ʥ��k���")] public Vector2 playerxLimit;
    [Header("��v�����k���")] public Vector2 cameraxLimit;

    void Start()
    {
        Initialize();
    }

    /// <summary>
    /// ��l��
    /// </summary>
    void Initialize()
    {
        playerxLimit = sceneData.playerxLimit;
        cameraxLimit = sceneData.cameraxLimit;
    }
}
