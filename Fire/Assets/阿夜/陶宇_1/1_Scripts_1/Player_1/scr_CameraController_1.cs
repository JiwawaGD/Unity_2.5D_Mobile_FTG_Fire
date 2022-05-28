using UnityEngine;

public class scr_CameraController_1 : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("���a")] Transform player;

    [SerializeField] [Header("�l�ܳt��")] float speed;

    [SerializeField] [Header("���k�譭��")] Vector2 x_limit;
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        player = FindObjectOfType<scr_PlayerController_1>().transform;
    }

    void LateUpdate()
    {
        Track();
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// ��v���l��
    /// </summary>
    void Track()
    {
        Vector3 v_camera = transform.position;  // 0, 1.5, -10
        Vector3 v_player = player.position;     // 0, -1.5, -3

        v_player.x = Mathf.Clamp(v_player.x, x_limit.x, x_limit.y);
        v_player.y = 0;
        v_player.z = -10;

        v_camera = Vector3.Lerp(v_camera, v_player, speed * Time.deltaTime);

        transform.localPosition = v_camera;
    }
    #endregion
}
