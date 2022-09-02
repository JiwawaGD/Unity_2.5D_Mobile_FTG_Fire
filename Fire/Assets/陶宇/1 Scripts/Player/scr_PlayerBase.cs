using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class scr_PlayerBase : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("������")] public scr_PlayerData playerdata;

    string enemyBody;
    string carbonNAtk;
    string carbonSkill;

    float gravity;               // �a�ߤޤO
    float jumpHeight;            // ���D���׭���
    float jumpForce;             // ���D�O�D

    Image hpBar;                 // ���
    Image rageBar;               // ����
    Image armorBar;              // �@�ұ�

    protected float hp;                       // �ͩR��
    protected float moveSpeed;                // ���ʳt��
    protected float jumpTimer;                // ���D - �p�ɾ�
    protected float attackInterval;           // �������j
    protected float attackTimer;              // ���� - �p�ɾ�
    protected float hurtTimer;                // ���� - �p�ɾ�
    protected float skillTimer;               // �ޯ� - �p�ɾ�
    protected float ultTimer;                 // �j�� - �p�ɾ�
    protected float armor;                    // �@�ҭ�

    protected int attackCount;                // �����p�ƾ�

    protected bool isJumping;                 // �O�_���D
    protected bool isSkilling;                // �I��ޯत
    protected bool isAttacking;               // ����I��    

    protected Button jump_btn;                // ���D - ���s
    protected Button attack_btn;              // ���� - ���s
    protected Button skill_1_btn;             // �ޯ�1 - ���s
    protected Button skill_2_btn;             // �ޯ�2 - ���s
    protected Button skill_3_btn;             // �ޯ�3 - ���s

    protected Vector3 moveDir;                // ���ʮy��
    protected scr_GameManager gameManager;    // �C���޲z��
    protected Animator ani;                   // �ʵe
    protected Rigidbody rig;                  // ����

    [HideInInspector] public float atk;              // �����O
    [HideInInspector] public float rage;             // ����
    [HideInInspector] public bool isUlt;             // �O�_�j�ۤ�
    [HideInInspector] public bool isDead;            // ���F
    [HideInInspector] public bool isGrounded;        // �O�_�b�a�W
    public bool faceRight;
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();

        hpBar = GameObject.Find("Canvas/HUD/���").GetComponent<Image>();
        armorBar = GameObject.Find("Canvas/HUD/�@�ұ�").GetComponent<Image>();
        rageBar = GameObject.Find("Canvas/HUD/����").GetComponent<Image>();

        jump_btn = GameObject.Find("�� - Btn").GetComponent<Button>();
        attack_btn = GameObject.Find("�� - Btn").GetComponent<Button>();
        skill_1_btn = GameObject.Find("�ޯ� 1 - Btn").GetComponent<Button>();
        skill_2_btn = GameObject.Find("�ޯ� 2 - Btn").GetComponent<Button>();
        skill_3_btn = GameObject.Find("�ޯ� 3 - Btn").GetComponent<Button>();

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
    /// �p�ɾ�
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
    /// ��l��
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
    /// �Ҧ�����
    /// </summary>
    void Movement()
    {
        // ����
        if (!gameManager.holding_left && !gameManager.holding_Right) Idle();

        // ����
        if (Input.GetKeyDown(KeyCode.Alpha5)) Attack();

        // ���m
        Defense();

        // ����
        if (Input.GetKey(KeyCode.A) || gameManager.holding_left) Move(new Vector3(-1, 0, 0), new Vector3(-1, 1, 1), false);
        if (Input.GetKey(KeyCode.D) || gameManager.holding_Right) Move(new Vector3(1, 0, 0), new Vector3(1, 1, 1), true);
    }

    /// <summary>
    /// �Ҧ����� - �����骺
    /// </summary>
    void Movement_Rig()
    {
        // �W�[�U�Y�t��
        if (transform.position.y >= jumpHeight) rig.velocity -= new Vector3(0, gravity * Time.deltaTime, 0);

        // ���եΥ\��
        if (Input.GetKey(KeyCode.Space)) SetJump();

        // ���D
        if (isJumping) Jump();
    }

    /// <summary>
    /// �P�_��
    /// </summary>
    protected virtual void Judgement()
    {
        if (jumpTimer >= 0.2f) isJumping = false;
        if (skillTimer <= 0) isSkilling = false;
        if (attackTimer >= attackInterval) attackCount = 0;
        if (hurtTimer >= 6f && armor <= playerdata.armorMax) armor += 1 * Time.deltaTime;
    }

    /// <summary>
    /// ���s�ƥ�
    /// </summary>
    protected virtual void ButtonOnclick() { }
    #endregion

    #region - Methods -
    /// <summary>
    /// ���D
    /// </summary>
    void Jump()
    {
        if (isGrounded) rig.velocity = new Vector3(0, jumpForce * Time.deltaTime * 60f, 0);
    }

    /// <summary>
    /// ��s��T
    /// </summary>
    void UpdateHUD()
    {
        hpBar.fillAmount = hp / playerdata.hpMax;
        armorBar.fillAmount = armor / playerdata.armorMax;
        rageBar.fillAmount = rage / playerdata.rageMax;
    }

    /// <summary>
    /// ���`
    /// </summary>
    protected void Die()
    {
        isDead = true;
        ani.SetBool("���� - Bool", false);
        ani.SetBool("���` - Bool", true);

        hpBar.fillAmount = 0;

        this.enabled = false;
    }

    /// <summary>
    /// ���ʥ\��
    /// </summary>
    /// <param name="direction">���ʮy��</param>
    /// <param name="scale">����ؤo</param>
    protected virtual void Move(Vector3 direction, Vector3 scale, bool _faceRight)
    {
        if (gameManager.holding_Defense ||
            isSkilling ||
            isAttacking) return;

        else
        {
            moveDir = direction;

            transform.Translate(moveDir * moveSpeed * Time.deltaTime);

            ani.SetBool("���� - Bool", true);

            transform.localScale = scale;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, gameManager.playerxLimit.x, gameManager.playerxLimit.y), transform.position.y, 0);
        faceRight = _faceRight;
    }

    /// <summary>
    /// ��ޯ�
    /// </summary>
    /// <param name="_name">�ޯ�W��</param>
    /// <param name="_time">�ʵe�ɪ�</param>
    /// <param name="_cost">�ޯ����</param>
    protected virtual void Skill(string _name, float _time, int _cost)
    {
        if (isSkilling) return;
        if (rage < _cost) return;

        isSkilling = true;
        ani.SetTrigger(_name);
        skillTimer = _time;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="damage">�ˮ`��</param>
    protected virtual void Hurt(int damage)
    {
        // ���m
        if (gameManager.holding_Defense && armor >= 0) armor -= damage;

        // �}�� or �����m
        else
        {
            hp -= damage;

            if (hp > 0) ani.SetTrigger("���� - Trigger");
        }

        hurtTimer = 0;

        // ���`
        if (hp <= 0) Die();

        Debug.Log("hp = " + hp);
        Debug.Log("armor = " + armor);
    }

    /// <summary>
    /// ����
    /// </summary>
    protected virtual void Idle()
    {
        moveDir = Vector3.zero;

        ani.SetBool("���� - Bool", false);
    }

    /// <summary>
    /// �]�w���D���A
    /// </summary>
    protected virtual void SetJump()
    {
        isJumping = true;

        jumpTimer = 0;
    }

    /// <summary>
    /// ����
    /// </summary>
    protected virtual void Attack() { }

    /// <summary>
    /// ���m
    /// </summary>
    protected virtual void Defense()
    {
        if (isSkilling) return;

        ani.SetBool("���m - Bool", gameManager.holding_Defense);
    }
    #endregion
}
