using UnityEngine;

public class scr_CameraController : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("追蹤速度")] float speed;

    [SerializeField] [Header("左右方限制")] Vector2 cameraxLimit;

    Transform player; // 玩家
    scr_GameManager gameManager; // 遊戲管理器
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
    /// 攝影機追蹤
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
