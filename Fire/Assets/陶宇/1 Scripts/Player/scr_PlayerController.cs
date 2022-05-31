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
    float ultTimer;              // 大招 - 計時器

    float hp;                    // 生命值
    float rage;                  // 怒氣值
    float armor;                 // 護甲值
    float atk;                   // 攻擊力
    int attackCount;             // 攻擊計數器

    bool isJumping;              // 是否跳躍
    bool isSkilling;             // 施放技能中
    bool isUlt;                  // 是否大招中

    Button jump_btn;             // 跳躍 - 按鈕
    Button attack_btn;           // 攻擊 - 按鈕
    Button skill_1_btn;          // 技能1 - 按鈕
    Button skill_2_btn;          // 技能2 - 按鈕
    Button skill_3_btn;          // 技能3 - 按鈕
    Image hpBar;                 // 血條
    Image rageBar;               // 怒氣條
    Image armorBar;              // 護甲條
    Vector3 moveDir;             // 移動座標

    Rigidbody rig;               // 剛體
    Animator ani;                // 動畫

    [HideInInspector] public bool isDead;            // 死了
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

        hpBar = GameObject.Find("HUD/血條").GetComponent<Image>();
        rageBar = GameObject.Find("HUD/怒氣條").GetComponent<Image>();
        armorBar = GameObject.Find("HUD/護甲條").GetComponent<Image>();

        jump_btn = GameObject.Find("跳__btn").GetComponent<Button>();
        attack_btn = GameObject.Find("攻__btn").GetComponent<Button>();
        skill_1_btn = GameObject.Find("技能1__Btn").GetComponent<Button>();
        skill_2_btn = GameObject.Find("技能2__Btn").GetComponent<Button>();
        skill_3_btn = GameObject.Find("技能3__Btn").GetComponent<Button>();
    }

    void Start()
    {
        Initialize();
        ButtonOnclick();
    }

    void Update()
    {
        Movement();
        Timer();
        Judgement();
        UpdateHUD();
    }

    void FixedUpdate()
    {
        Movement_Rig();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "可使玩家受傷") Hurt(10);
    }
    #endregion

    #region - Event -
    /// <summary>
    /// 計時器
    /// </summary>
    void Timer()
    {
        jumpTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        hurtTimer += Time.deltaTime;
        skillTimer -= Time.deltaTime;
        ultTimer -= Time.deltaTime;
    }

    /// <summary>
    /// 判斷式
    /// </summary>
    void Judgement()
    {
        if (jumpTimer >= 0.2f) isJumping = false;
        if (skillTimer <= 0) isSkilling = false;
        if (ultTimer <= 0) isUlt = false;
        if (attackTimer >= attackInterval) attackCount = 0;
        ani.SetBool("技能3 - 狀態 - Bool", isUlt);
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        holding_left = false;
        holding_Right = false;
        isDefense = false;
        isDead = false;
        isSkilling = false;
        isGrounded = true;

        hp = data.hp;
        armor = data.armor;
        atk = data.atk;

        rage = 0f;
        gravity = 150f;
        jumpHeight = 3f;
        jumpForce = 30f;
        moveSpeed = 8f;
        attackCount = 0;
        attackInterval = 2f;
    }

    /// <summary>
    /// 按鈕事件
    /// </summary>
    void ButtonOnclick()
    {
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);

        skill_1_btn.onClick.AddListener(() => { Skill("技能1 - Trigger", data.playerSkills[0].time, data.playerSkills[0].cost); });
        skill_2_btn.onClick.AddListener(() => { Skill("技能2 - Trigger", data.playerSkills[1].time, data.playerSkills[1].cost); });
        skill_3_btn.onClick.AddListener(() => { Skill("技能3 - Trigger", data.playerSkills[2].time, data.playerSkills[2].cost); });
        skill_3_btn.onClick.AddListener(Ultimate);
    }

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
    /// 所有移動 - 有剛體的
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

        if (isUlt) return;

        ani.SetBool("防禦 - Bool", press);
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// 移動功能
    /// </summary>
    /// <param name="direction">移動座標</param>
    /// <param name="scale">物件尺寸</param>
    void Move(Vector3 direction, Vector3 scale)
    {
        if (isDefense || isSkilling) return;

        else
        {
            moveDir = direction;

            rig.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

            if (isUlt) ani.SetBool("技能3 - 移動 - Bool", true);

            else ani.SetBool("移動 - Bool", true);

            transform.localScale = scale;
        }

        //transform.position = Mathf.Clamp((transform.position.x,))
    }

    /// <summary>
    /// 放技能
    /// </summary>
    /// <param name="_name">技能名稱</param>
    /// <param name="_time">動畫時長</param>
    /// <param name="_cost">技能消耗</param>
    void Skill(string _name, float _time, int _cost)
    {
        if (isUlt || isSkilling) return;
        if (rage < _cost) return;

        isSkilling = true;
        ani.SetTrigger(_name);
        skillTimer = _time;
    }

    /// <summary>
    /// 使用大招
    /// </summary>
    void Ultimate()
    {
        isUlt = true;

        ultTimer = 10f;
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">傷害值</param>
    void Hurt(int damage)
    {
        if (isUlt) return;

        // 防禦
        if (isDefense && armor >= 0) armor -= damage;

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
        ani.SetBool("技能3 - 移動 - Bool", false);
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
        isDead = true;
        ani.SetBool("移動 - Bool", false);
        ani.SetBool("死亡 - Bool", true);

        hpBar.fillAmount = 0;

        this.enabled = false;
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    void Attack()
    {
        if (isSkilling) return;

        if (isUlt && attackTimer > 1.417f)
        {
            ani.SetTrigger("技能3 - 攻擊 - Trigger");
            attackTimer = 0;
        }

        if (attackCount == 2 && attackTimer > data.attackTime[2])
        {
            ani.SetTrigger("攻擊3 - Trigger");
            attackCount = 0;
            attackTimer = 0;
        }
        else if (attackCount == 1 && attackTimer > data.attackTime[1])
        {
            ani.SetTrigger("攻擊2 - Trigger");
            attackCount += 1;
            attackTimer = 0;
        }
        else if (attackCount == 0 && attackTimer > data.attackTime[0])
        {
            ani.SetTrigger("攻擊1 - Trigger");
            attackCount += 1;
            attackTimer = 0;
        }
    }

    /// <summary>
    /// 防禦
    /// </summary>
    void Defense()
    {
        if (isSkilling || isUlt) return;

        if (hurtTimer >= 6f && armor <= data.armor)
        {
            armor += 5;
        }
    }

    /// <summary>
    /// 更新資訊
    /// </summary>
    void UpdateHUD()
    {
        hpBar.fillAmount = hp / data.hp;
        armorBar.fillAmount = armor / data.armor;
        rageBar.fillAmount = rage / data.rage;
    }
    #endregion
}
