using UnityEngine;

public class scr_CameraController : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("�l�ܳt��")] float speed;

    [SerializeField] [Header("���k�譭��")] Vector2 cameraxLimit;

    Transform player; // ���a
    scr_GameManager gameManager; // �C���޲z��
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        player = FindObjectOfType<scr_PlayerBase>().transform;
        gameManager = GameObject.Find("GameManager").GetComponent<scr_GameManager>();
    }

    void Start()
    {
        Initialize();
    }

    void LateUpdate()
    {
        Track();
    }
    #endregion

    #region - Methods -

    void Initialize()
    {
        cameraxLimit = gameManager.cameraxLimit;
    }

    /// <summary>
    /// ��v���l��
    /// </summary>
    void Track()
    {
        Vector3 v_camera = transform.position;  // 0, 1.5, -10
        Vector3 v_player = player.position;     // 0, -1.5, -3

        v_player.x = Mathf.Clamp(v_player.x, cameraxLimit.x, cameraxLimit.y);
        v_player.y = 0;
        v_player.z = -10;

        v_camera = Vector3.Lerp(v_camera, v_player, speed * Time.deltaTime);

        transform.localPosition = v_camera;
    }
    #endregion
}
