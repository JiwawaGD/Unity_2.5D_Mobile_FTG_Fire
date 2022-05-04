using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerController : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("角色資料")] scr_PlayerData data;

    float gravity;               // 地心引力
    float moveSpeed;             // 移動速度
    float jumpHeight;            // 跳躍高度限制
    float jumpForce;             // 跳躍力道
    float jumpTimer;             // 跳躍 - 計時器
    float attackInterval;        // 攻擊間隔
    float attackTimer;           // 攻擊 - 計時器
    float hurtTimer;             // 受傷 - 計時器
    float skillTimer;            // 技能 - 計時器

    int hp;                      // 生命值
    int armor;                   // 護甲值
    int attackCount;             // 攻擊計數器

    bool isJumping;              // 是否跳躍

    Button jump_btn;             // 跳躍 - 按鈕
    Button attack_btn;           // 攻擊 - 按鈕
    Button skill_1_btn;          // 技能1 - 按鈕
    Vector3 moveDir;             // 移動座標
    Rigidbody rig;               // 剛體
    Animator ani;                // 動畫

    [HideInInspector] public bool holding_left;      // 按下左鍵
    [HideInInspector] public bool holding_Right;     // 按下左鍵
    [HideInInspector] public bool isDefense;         // 按下防禦
    [HideInInspector] public bool isGrounded;        // 是否在地上
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();

        jump_btn = GameObject.Find("跳__btn").GetComponent<Button>();
        attack_btn = GameObject.Find("攻__btn").GetComponent<Button>();
        skill_1_btn = GameObject.Find("技能1__Btn").GetComponent<Button>();
    }

    void Start()
    {
        holding_left = false;
        holding_Right = false;
        isDefense = false;
        isGrounded = true;

        moveSpeed = data.moveSpeed;
        jumpForce = data.jumpForce;
        jumpHeight = data.jumpHeight;
        hp = data.hp;
        armor = data.armor;

        gravity = 150f;
        attackCount = 0;
        attackInterval = 2f;

        // 按鈕
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);
        skill_1_btn.onClick.AddListener(() => { Skill("技能1 - Trigger", 1.667f); });
    }

    void Update()
    {
        Movement();

        // 計時器
        jumpTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        skillTimer += Time.deltaTime;
        hurtTimer += Time.deltaTime;

        // 判斷式
        if (jumpTimer >= 0.2f) isJumping = false;
        if (attackTimer >= attackInterval) attackCount = 0;
    }

    void FixedUpdate()
    {
        Movement_Rig();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "可使玩家受傷") Hurt(1);
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

    /// <summary>
    /// 按著防禦
    /// </summary>
    /// <param name="press">是否按著</param>
    public void HoldDefense(bool press)
    {
        isDefense = press;
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// 所有移動
    /// </summary>
    void Movement()
    {
        // 等待
        if (!holding_left && !holding_Right) Idle();

        // 攻擊
        if (Input.GetKeyDown(KeyCode.Alpha5)) Attack();

        // 防禦
        if (isDefense) Defense();
    }

    /// <summary>
    /// 所有移動 - 有缸體的
    /// </summary>
    void Movement_Rig()
    {
        // 增加下墜速度
        if (transform.position.y >= jumpHeight) rig.velocity -= new Vector3(0, gravity * Time.deltaTime, 0);

        // 移動
        if (holding_left) Move(new Vector3(-1, 0, 0), new Vector3(-1, 1, 1));
        if (holding_Right) Move(new Vector3(1, 0, 0), new Vector3(1, 1, 1));

        // 測試用功能
        if (Input.GetKey(KeyCode.A)) Move(new Vector3(-1, 0, 0), new Vector3(-1, 1, 1));
        if (Input.GetKey(KeyCode.D)) Move(new Vector3(1, 0, 0), new Vector3(1, 1, 1));
        if (Input.GetKey(KeyCode.Space)) SetJump();

        // 跳躍
        if (isJumping) Jump();
    }

    /// <summary>
    /// 移動功能
    /// </summary>
    /// <param name="direction">移動座標</param>
    /// <param name="scale">物件尺寸</param>
    void Move(Vector3 direction, Vector3 scale)
    {
        // 防禦中不可動
        if (isDefense) return;

        else
        {
            moveDir = direction;

            rig.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

            ani.SetBool("移動 - Bool", true);

            transform.localScale = scale;
        }
    }

    /// <summary>
    /// 施放技能
    /// </summary>
    /// <param name="skillname">技能名稱</param>
    /// <param name="skilltime">技能時間</param>
    void Skill(string skillname, float skilltime)
    {
        if (skillTimer > skilltime)
        {
            ani.SetTrigger(skillname);
            skillTimer = 0;
        }
        else return;
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">傷害值</param>
    void Hurt(int damage)
    {
        // 防禦
        if (isDefense && armor >= 0)
        {
            armor -= damage;
        }
        // 破防 or 未防禦
        else
        {
            hp -= damage;

            if (hp > 0) ani.SetTrigger("受傷 - Trigger");
        }

        hurtTimer = 0;

        // 死亡
        if (hp <= 0) Die();

        Debug.Log("hp = ");
        Debug.Log(hp);
        Debug.Log("armor = ");
        Debug.Log(armor);
    }

    /// <summary>
    /// 等待
    /// </summary>
    void Idle()
    {
        moveDir = Vector3.zero;

        ani.SetBool("移動 - Bool", false);
    }

    /// <summary>
    /// 設定跳躍狀態
    /// </summary>
    void SetJump()
    {
        isJumping = true;

        jumpTimer = 0;
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    void Jump()
    {
        if (isGrounded) rig.velocity = new Vector3(0, jumpForce * Time.deltaTime * 60f, 0);
    }

    /// <summary>
    /// 死亡
    /// </summary>
    void Die()
    {
        ani.SetBool("死亡 - Bool", true);

        this.enabled = false;
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    void Attack()
    {
        if (attackCount == 2 && attackTimer > 0.78f)
        {
            ani.SetTrigger("攻擊3 - Trigger");

            attackTimer = 0;
            attackCount = 0;
        }
        else if (attackCount == 1 && attackTimer > 0.68f)
        {
            ani.SetTrigger("攻擊2 - Trigger");

            attackTimer = 0;
            attackCount += 1;
        }
        else if (attackCount == 0 && attackTimer > 0.68f)
        {
            ani.SetTrigger("攻擊1 - Trigger");

            attackTimer = 0;
            attackCount += 1;
        }
    }

    /// <summary>
    /// 防禦
    /// </summary>
    void Defense()
    {
        Debug.Log("is defensing");

        if (hurtTimer >= 20f && armor <= data.armor)
        {
            armor += 1;
        }
    }
    #endregion
}
