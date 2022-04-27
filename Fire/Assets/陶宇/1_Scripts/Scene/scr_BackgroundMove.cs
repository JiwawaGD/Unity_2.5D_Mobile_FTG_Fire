using UnityEngine;

public class scr_BackgroundMove : MonoBehaviour
{
    [SerializeField] [Header("可移動的背景")] bool movable;

    [SerializeField] [Header("移動速度")] float speed;

    [SerializeField] [Header("後景左右限制 / 前中景省略")] Vector2 backLimit;

    GameObject player;          // 玩家物件
    GameObject backObject;      // 後景物件
    scr_PlayerController playerController;

    void Awake()
    {
        player = GameObject.Find("吉娃娃_Player");
        backObject = GameObject.Find("背景_Sprites/後景");
        playerController = player.GetComponent<scr_PlayerController>();
    }

    void Start()
    {
        backLimit.x = -31f;
        backLimit.y = 31f;
    }

    void Update()
    {
        Track(speed);
    }

    /// <summary>
    /// 背景移動
    /// </summary>
    /// <param name="distance">移動量</param>
    void Track(float distance)
    {
        Vector3 temp = transform.localPosition;
        Vector3 target = player.transform.localPosition;

        // 前中景
        if (movable)
        {
            if (backObject.transform.position.x == backLimit.x || backObject.transform.position.x == backLimit.y) return;

            else
            {
                temp.z = 0;

                if (playerController.holding_left) temp.x -= distance * Time.deltaTime;

                else if (playerController.holding_Right) temp.x += distance * Time.deltaTime;
            }
        }
        // 後景
        else if (!movable)
        {
            target.x = Mathf.Clamp(target.x, backLimit.x, backLimit.y);
            target.y = 2.4f;
            target.z = 0;

            temp = target;
        }
        transform.localPosition = temp;
    }
}
