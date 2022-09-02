using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class scr_PlayerBase : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("角色資料")] public scr_PlayerData playerdata;

    string enemyBody;
    string carbonNAtk;
    string carbonSkill;

    float gravity;               // 地心引力
    float jumpHeight;            // 跳躍高度限制
    float jumpForce;             // 跳躍力道

    Image hpBar;                 // 血條
    Image rageBar;               // 怒氣條
    Image armorBar;              // 護甲條

    protected float hp;                       // 生命值
    protected float moveSpeed;                // 移動速度
    protected float jumpTimer;                // 跳躍 - 計時器
    protected float attackInterval;           // 攻擊間隔
    protected float attackTimer;              // 攻擊 - 計時器
    protected float hurtTimer;                // 受傷 - 計時器
    protected float skillTimer;               // 技能 - 計時器
    protected float ultTimer;                 // 大招 - 計時器
    protected float armor;                    // 護甲值

    protected int attackCount;                // 攻擊計數器

    protected bool isJumping;                 // 是否跳躍
    protected bool isSkilling;                // 施放技能中
    protected bool isAttacking;               // 普攻施放中    

    protected Button jump_btn;                // 跳躍 - 按鈕
    protected Button attack_btn;              // 攻擊 - 按鈕
    protected Button skill_1_btn;             // 技能1 - 按鈕
    protected Button skill_2_btn;             // 技能2 - 按鈕
    protected Button skill_3_btn;             // 技能3 - 按鈕

    protected Vector3 moveDir;                // 移動座標
    protected scr_GameManager gameManager;    // 遊戲管理器
    protected Animator ani;                   // 動畫
    protected Rigidbody rig;                  // 剛體

    [HideInInspector] public float atk;              // 攻擊力
    [HideInInspector] public float rage;             // 怒氣值
    [HideInInspector] public bool isUlt;             // 是否大招中
    [HideInInspector] public bool isDead;            // 死了
    [HideInInspector] public bool isGrounded;        // 是否在地上
    public bool faceRight;
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();

        hpBar = GameObject.Find("Canvas/HUD/血條").GetComponent<Image>();
        armorBar = GameObject.Find("Canvas/HUD/護甲條").GetComponent<Image>();
        rageBar = GameObject.Find("Canvas/HUD/怒氣條").GetComponent<Image>();

        jump_btn = GameObject.Find("跳 - Btn").GetComponent<Button>();
        attack_btn = GameObject.Find("攻 - Btn").GetComponent<Button>();
        skill_1_btn = GameObject.Find("技能 1 - Btn").GetComponent<Button>();
        skill_2_btn = GameObject.Find("技能 2 - Btn").GetComponent<Button>();
        skill_3_btn = GameObject.Find("技能 3 - Btn").GetComponent<Button>();

        gameManager = GameObject.Find("GameManager").GetComponent<scr_GameManager>();
    }

    protected virtual void Start()
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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag.Contains(enemyBody))
        {
            Hurt(5);
        }
        else if (col.gameObject.tag.Contains(carbonNAtk))
        {
            Hurt(10);
        }
        else if (col.gameObject.tag.Contains(carbonSkill))
        {
            Hurt(20);
        }
    }
    #endregion

    #region - Event -
    /// <summary>
    /// 計時器
    /// </summary>
    protected virtual void Timer()
    {
        if (jumpTimer <= 2f) jumpTimer += Time.deltaTime;
        if (attackTimer <= 5f) attackTimer += Time.deltaTime;
        if (hurtTimer <= 8f) hurtTimer += Time.deltaTime;

        if (skillTimer >= -2f) skillTimer -= Time.deltaTime;
        if (ultTimer >= -2f) ultTimer -= Time.deltaTime;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        enemyBody = "EnemyBodyCollider";
        carbonNAtk = "CarbonNATK";
        carbonSkill = "CarbonElectricity";

        isDead = false;
        isSkilling = false;
        isGrounded = true;
        faceRight = true;

        hp = playerdata.hp;
        armor = playerdata.armor;
        atk = playerdata.atk;
        rage = playerdata.rage;

        gravity = 150f;
        jumpHeight = 3f;
        jumpForce = 40f;
        moveSpeed = 8f;
        attackCount = 0;
        attackInterval = 2f;
    }

    /// <summary>
    /// 所有移動
    /// </summary>
    void Movement()
    {
        // 等待
        if (!gameManager.holding_left && !gameManager.holding_Right) Idle();

        // 攻擊
        if (Input.GetKeyDown(KeyCode.Alpha5)) Attack();

        // 防禦
        Defense();

        // 移動
        if (Input.GetKey(KeyCode.A) || gameManager.holding_left) Move(new Vector3(-1, 0, 0), new Vector3(-1, 1, 1), false);
        if (Input.GetKey(KeyCode.D) || gameManager.holding_Right) Move(new Vector3(1, 0, 0), new Vector3(1, 1, 1), true);
    }

    /// <summary>
    /// 所有移動 - 有剛體的
    /// </summary>
    void Movement_Rig()
    {
        // 增加下墜速度
        if (transform.position.y >= jumpHeight) rig.velocity -= new Vector3(0, gravity * Time.deltaTime, 0);

        // 測試用功能
        if (Input.GetKey(KeyCode.Space)) SetJump();

        // 跳躍
        if (isJumping) Jump();
    }

    /// <summary>
    /// 判斷式
    /// </summary>
    protected virtual void Judgement()
    {
        if (jumpTimer >= 0.2f) isJumping = false;
        if (skillTimer <= 0) isSkilling = false;
        if (attackTimer >= attackInterval) attackCount = 0;
        if (hurtTimer >= 6f && armor <= playerdata.armorMax) armor += 1 * Time.deltaTime;
    }

    /// <summary>
    /// 按鈕事件
    /// </summary>
    protected virtual void ButtonOnclick() { }
    #endregion

    #region - Methods -
    /// <summary>
    /// 跳躍
    /// </summary>
    void Jump()
    {
        if (isGrounded) rig.velocity = new Vector3(0, jumpForce * Time.deltaTime * 60f, 0);
    }

    /// <summary>
    /// 更新資訊
    /// </summary>
    void UpdateHUD()
    {
        hpBar.fillAmount = hp / playerdata.hpMax;
        armorBar.fillAmount = armor / playerdata.armorMax;
        rageBar.fillAmount = rage / playerdata.rageMax;
    }

    /// <summary>
    /// 死亡
    /// </summary>
    protected void Die()
    {
        isDead = true;
        ani.SetBool("移動 - Bool", false);
        ani.SetBool("死亡 - Bool", true);

        hpBar.fillAmount = 0;

        this.enabled = false;
    }

    /// <summary>
    /// 移動功能
    /// </summary>
    /// <param name="direction">移動座標</param>
    /// <param name="scale">物件尺寸</param>
    protected virtual void Move(Vector3 direction, Vector3 scale, bool _faceRight)
    {
        if (gameManager.holding_Defense ||
            isSkilling ||
            isAttacking) return;

        else
        {
            moveDir = direction;

            transform.Translate(moveDir * moveSpeed * Time.deltaTime);

            ani.SetBool("移動 - Bool", true);

            transform.localScale = scale;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, gameManager.playerxLimit.x, gameManager.playerxLimit.y), transform.position.y, 0);
        faceRight = _faceRight;
    }

    /// <summary>
    /// 放技能
    /// </summary>
    /// <param name="_name">技能名稱</param>
    /// <param name="_time">動畫時長</param>
    /// <param name="_cost">技能消耗</param>
    protected virtual void Skill(string _name, float _time, int _cost)
    {
        if (isSkilling) return;
        if (rage < _cost) return;

        isSkilling = true;
        ani.SetTrigger(_name);
        skillTimer = _time;
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">傷害值</param>
    protected virtual void Hurt(int damage)
    {
        // 防禦
        if (gameManager.holding_Defense && armor >= 0) armor -= damage;

        // 破防 or 未防禦
        else
        {
            hp -= damage;

            if (hp > 0) ani.SetTrigger("受傷 - Trigger");
        }

        hurtTimer = 0;

        // 死亡
        if (hp <= 0) Die();

        Debug.Log("hp = " + hp);
        Debug.Log("armor = " + armor);
    }

    /// <summary>
    /// 等待
    /// </summary>
    protected virtual void Idle()
    {
        moveDir = Vector3.zero;

        ani.SetBool("移動 - Bool", false);
    }

    /// <summary>
    /// 設定跳躍狀態
    /// </summary>
    protected virtual void SetJump()
    {
        isJumping = true;

        jumpTimer = 0;
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    protected virtual void Attack() { }

    /// <summary>
    /// 防禦
    /// </summary>
    protected virtual void Defense()
    {
        if (isSkilling) return;

        ani.SetBool("防禦 - Bool", gameManager.holding_Defense);
    }
    #endregion
}
