using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerController : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("角色資料")] scr_PlayerData data;

    [SerializeField] [Header("地心引力")] float gravity;

    float moveSpeed;             // 移動速度
    float jumpHeight;            // 跳躍高度限制
    float jumpForce;             // 跳躍力道
    float hp;                    // 生命值

    bool holding_left = false;   // 按下左鍵
    bool holding_Right = false;  // 按下左鍵

    Button jump_btn;             // 跳躍 - 按鈕
    [SerializeField] Vector3 moveDir;             // 移動座標
    Rigidbody rig;               // 剛體
    [SerializeField] Animator ani;

    [HideInInspector] public bool isGrounded;
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        jump_btn = GameObject.Find("跳_btn").GetComponent<Button>();
    }

    void Start()
    {
        holding_left = false;
        holding_Right = false;
        isGrounded = true;

        moveSpeed = data.moveSpeed;
        jumpForce = data.jumpForce;
        jumpHeight = data.jumpHeight;
        hp = data.hp;
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "可使玩家受傷")
        {
            Hurt(1);
        }
    }
    #endregion

    #region - Button trigger -
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
    #endregion

    #region - Methods -
    /// <summary>
    /// 所有與鋼體有關移動
    /// </summary>
    void Movement()
    {
        // 增加下墜速度
        if (transform.position.y >= jumpHeight) rig.velocity -= new Vector3(0, gravity * Time.deltaTime, 0);

        // 等待
        if (!holding_left && !holding_Right) Idle();

        // 移動
        if (holding_left || Input.GetKey(KeyCode.A)) Move(new Vector3(-1, 0, 0), new Vector3(-1f, 1, 1));
        if (holding_Right || Input.GetKey(KeyCode.D)) Move(new Vector3(1, 0, 0), new Vector3(1, 1, 1));

        // 移動動畫
        ani.SetBool("移動 - Bool", moveDir != Vector3.zero);

        // 跳躍
        if (Input.GetKey(KeyCode.Space)) Jump();
        jump_btn.onClick.AddListener(Jump);
    }

    /// <summary>
    /// 等待
    /// </summary>
    void Idle()
    {
        moveDir = Vector3.zero;
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

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">傷害值</param>
    void Hurt(float damage)
    {
        hp -= damage;
        Debug.Log(hp);

        // 生命值歸零 > 死亡
        if (hp <= 0) Die();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    void Die()
    {
        this.enabled = false;
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    void Attack()
    {

    }
    #endregion
}
