using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerController : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("移動速度")] float moveSpeed;
    [SerializeField] [Header("跳躍力道")] float jumpForce;
    [SerializeField] [Header("跳躍高度限制")] float jumpHeight;
    [SerializeField] [Header("地心引力")] float gravity;

    [SerializeField] [Header("跳躍 - 按鈕")] Button jump_btn;

    bool holding_left = false;
    bool holding_Right = false;
    public bool isGrounded;

    Vector3 moveDir;
    Rigidbody rig;
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        jump_btn = GameObject.Find("跳_btn").GetComponent<Button>();
    }

    void Start()
    {
        holding_left = false;
        holding_Right = false;
        isGrounded = true;
    }

    void FixedUpdate()
    {
        Movement();
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// 按著左鍵
    /// </summary>
    /// <param name="press">是否按著</param>
    public void HoldLeft(bool press)
    {
        holding_left = press;
    }

    /// <summary>
    /// 按著右鍵
    /// </summary>
    /// <param name="press">是否按著</param>
    public void HoldRight(bool press)
    {
        holding_Right = press;
    }

    /// <summary>
    /// 所有與鋼體有關移動
    /// </summary>
    void Movement()
    {
        // 移動
        if (holding_left) Move(new Vector3(-2, 0, 0), new Vector3(-1f, 1, 1));

        if (holding_Right) Move(new Vector3(2, 0, 0), new Vector3(1, 1, 1));

        if (!holding_left && !holding_Right) moveDir = Vector3.zero;

        // 跳躍
        jump_btn.onClick.AddListener(Jump);

        // 增加下墜速度
        if (transform.position.y >= jumpHeight) rig.velocity -= new Vector3(0, gravity * Time.deltaTime, 0);
    }

    /// <summary>
    /// 移動功能
    /// </summary>
    /// <param name="direction">移動座標</param>
    /// <param name="scale">物件尺寸</param>
    void Move(Vector3 direction, Vector3 scale)
    {
        moveDir = direction;

        rig.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

        transform.localScale = scale;
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    void Jump()
    {
        if (isGrounded) rig.velocity = new Vector3(0, jumpForce, 0);
        else return;
    }
    #endregion
}
