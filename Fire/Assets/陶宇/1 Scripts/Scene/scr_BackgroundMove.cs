using UnityEngine;

public class scr_BackgroundMove : MonoBehaviour
{
    [SerializeField] [Header("可移動的背景")] bool movable;

    [SerializeField] [Header("移動速度")] float speed;

    [SerializeField] [Header("後景左右限制 / 前中景省略")] Vector2 backLimit;

    GameObject backObject;      // 後景物件
    scr_PlayerBase playerBase;
    scr_GameManager gameManager;

    void Awake()
    {
        playerBase = FindObjectOfType<scr_PlayerBase>();
        gameManager = FindObjectOfType<scr_GameManager>();
        backObject = GameObject.Find("背景/後景");
    }

    void Start()
    {
        backLimit.x = -31f;
        backLimit.y = 41f;
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
        Vector3 target = playerBase.transform.localPosition;

        if (playerBase.isDead) return;

        else
        {
            // 前中景
            if (movable)
            {
                if (backObject.transform.position.x == backLimit.x || backObject.transform.position.x == backLimit.y) return;

                else
                {
                    temp.z = 0;

                    if (gameManager.holding_left) temp.x -= distance * Time.deltaTime;

                    else if (gameManager.holding_Right) temp.x += distance * Time.deltaTime;
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
}
