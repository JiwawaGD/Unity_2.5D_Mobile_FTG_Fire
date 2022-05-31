using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerController : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("������")] scr_PlayerData data;

    float gravity;               // �a�ߤޤO
    float moveSpeed;             // ���ʳt��
    float jumpHeight;            // ���D���׭���
    float jumpForce;             // ���D�O�D
    float jumpTimer;             // ���D - �p�ɾ�
    float attackInterval;        // �������j
    float attackTimer;           // ���� - �p�ɾ�
    float hurtTimer;             // ���� - �p�ɾ�
    float skillTimer;            // �ޯ� - �p�ɾ�
    float ultTimer;              // �j�� - �p�ɾ�

    float hp;                    // �ͩR��
    float rage;                  // ����
    float armor;                 // �@�ҭ�
    float atk;                   // �����O
    int attackCount;             // �����p�ƾ�

    bool isJumping;              // �O�_���D
    bool isSkilling;             // �I��ޯत
    bool isUlt;                  // �O�_�j�ۤ�

    Button jump_btn;             // ���D - ���s
    Button attack_btn;           // ���� - ���s
    Button skill_1_btn;          // �ޯ�1 - ���s
    Button skill_2_btn;          // �ޯ�2 - ���s
    Button skill_3_btn;          // �ޯ�3 - ���s
    Image hpBar;                 // ���
    Image rageBar;               // ����
    Image armorBar;              // �@�ұ�
    Vector3 moveDir;             // ���ʮy��

    Rigidbody rig;               // ����
    Animator ani;                // �ʵe

    [HideInInspector] public bool isDead;            // ���F
    [HideInInspector] public bool holding_left;      // ���U����
    [HideInInspector] public bool holding_Right;     // ���U����
    [HideInInspector] public bool isDefense;         // ���U���m
    [HideInInspector] public bool isGrounded;        // �O�_�b�a�W
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();

        hpBar = GameObject.Find("HUD/���").GetComponent<Image>();
        rageBar = GameObject.Find("HUD/����").GetComponent<Image>();
        armorBar = GameObject.Find("HUD/�@�ұ�").GetComponent<Image>();

        jump_btn = GameObject.Find("��__btn").GetComponent<Button>();
        attack_btn = GameObject.Find("��__btn").GetComponent<Button>();
        skill_1_btn = GameObject.Find("�ޯ�1__Btn").GetComponent<Button>();
        skill_2_btn = GameObject.Find("�ޯ�2__Btn").GetComponent<Button>();
        skill_3_btn = GameObject.Find("�ޯ�3__Btn").GetComponent<Button>();
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
        if (col.tag == "�i�Ϫ��a����") Hurt(10);
    }
    #endregion

    #region - Event -
    /// <summary>
    /// �p�ɾ�
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
    /// �P�_��
    /// </summary>
    void Judgement()
    {
        if (jumpTimer >= 0.2f) isJumping = false;
        if (skillTimer <= 0) isSkilling = false;
        if (ultTimer <= 0) isUlt = false;
        if (attackTimer >= attackInterval) attackCount = 0;
        ani.SetBool("�ޯ�3 - ���A - Bool", isUlt);
    }

    /// <summary>
    /// ��l��
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
    /// ���s�ƥ�
    /// </summary>
    void ButtonOnclick()
    {
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);

        skill_1_btn.onClick.AddListener(() => { Skill("�ޯ�1 - Trigger", data.playerSkills[0].time, data.playerSkills[0].cost); });
        skill_2_btn.onClick.AddListener(() => { Skill("�ޯ�2 - Trigger", data.playerSkills[1].time, data.playerSkills[1].cost); });
        skill_3_btn.onClick.AddListener(() => { Skill("�ޯ�3 - Trigger", data.playerSkills[2].time, data.playerSkills[2].cost); });
        skill_3_btn.onClick.AddListener(Ultimate);
    }

    /// <summary>
    /// �Ҧ�����
    /// </summary>
    void Movement()
    {
        // ����
        if (!holding_left && !holding_Right) Idle();

        // ����
        if (Input.GetKeyDown(KeyCode.Alpha5)) Attack();

        // ���m
        if (isDefense) Defense();
    }

    /// <summary>
    /// �Ҧ����� - �����骺
    /// </summary>
    void Movement_Rig()
    {
        // �W�[�U�Y�t��
        if (transform.position.y >= jumpHeight) rig.velocity -= new Vector3(0, gravity * Time.deltaTime, 0);

        // ����
        if (holding_left) Move(new Vector3(-1, 0, 0), new Vector3(-1, 1, 1));
        if (holding_Right) Move(new Vector3(1, 0, 0), new Vector3(1, 1, 1));

        // ���եΥ\��
        if (Input.GetKey(KeyCode.A)) Move(new Vector3(-1, 0, 0), new Vector3(-1, 1, 1));
        if (Input.GetKey(KeyCode.D)) Move(new Vector3(1, 0, 0), new Vector3(1, 1, 1));
        if (Input.GetKey(KeyCode.Space)) SetJump();

        // ���D
        if (isJumping) Jump();
    }
    #endregion

    #region - Button trigger -
    /// <summary>
    /// ���ۥ���
    /// </summary>
    /// <param name="press">�O�_����</param>
    public void HoldLeft(bool press)
    {
        holding_left = press;
    }

    /// <summary>
    /// ���ۥk��
    /// </summary>
    /// <param name="press">�O�_����</param>
    public void HoldRight(bool press)
    {
        holding_Right = press;
    }

    /// <summary>
    /// ���ۨ��m
    /// </summary>
    /// <param name="press">�O�_����</param>
    public void HoldDefense(bool press)
    {
        isDefense = press;

        if (isUlt) return;

        ani.SetBool("���m - Bool", press);
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// ���ʥ\��
    /// </summary>
    /// <param name="direction">���ʮy��</param>
    /// <param name="scale">����ؤo</param>
    void Move(Vector3 direction, Vector3 scale)
    {
        if (isDefense || isSkilling) return;

        else
        {
            moveDir = direction;

            rig.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

            if (isUlt) ani.SetBool("�ޯ�3 - ���� - Bool", true);

            else ani.SetBool("���� - Bool", true);

            transform.localScale = scale;
        }

        //transform.position = Mathf.Clamp((transform.position.x,))
    }

    /// <summary>
    /// ��ޯ�
    /// </summary>
    /// <param name="_name">�ޯ�W��</param>
    /// <param name="_time">�ʵe�ɪ�</param>
    /// <param name="_cost">�ޯ����</param>
    void Skill(string _name, float _time, int _cost)
    {
        if (isUlt || isSkilling) return;
        if (rage < _cost) return;

        isSkilling = true;
        ani.SetTrigger(_name);
        skillTimer = _time;
    }

    /// <summary>
    /// �ϥΤj��
    /// </summary>
    void Ultimate()
    {
        isUlt = true;

        ultTimer = 10f;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="damage">�ˮ`��</param>
    void Hurt(int damage)
    {
        if (isUlt) return;

        // ���m
        if (isDefense && armor >= 0) armor -= damage;

        // �}�� or �����m
        else
        {
            hp -= damage;

            if (hp > 0) ani.SetTrigger("���� - Trigger");
        }

        hurtTimer = 0;

        // ���`
        if (hp <= 0) Die();

        Debug.Log("hp = ");
        Debug.Log(hp);
        Debug.Log("armor = ");
        Debug.Log(armor);
    }

    /// <summary>
    /// ����
    /// </summary>
    void Idle()
    {
        moveDir = Vector3.zero;

        ani.SetBool("���� - Bool", false);
        ani.SetBool("�ޯ�3 - ���� - Bool", false);
    }

    /// <summary>
    /// �]�w���D���A
    /// </summary>
    void SetJump()
    {
        isJumping = true;

        jumpTimer = 0;
    }

    /// <summary>
    /// ���D
    /// </summary>
    void Jump()
    {
        if (isGrounded) rig.velocity = new Vector3(0, jumpForce * Time.deltaTime * 60f, 0);
    }

    /// <summary>
    /// ���`
    /// </summary>
    void Die()
    {
        isDead = true;
        ani.SetBool("���� - Bool", false);
        ani.SetBool("���` - Bool", true);

        hpBar.fillAmount = 0;

        this.enabled = false;
    }

    /// <summary>
    /// ����
    /// </summary>
    void Attack()
    {
        if (isSkilling) return;

        if (isUlt && attackTimer > 1.417f)
        {
            ani.SetTrigger("�ޯ�3 - ���� - Trigger");
            attackTimer = 0;
        }

        if (attackCount == 2 && attackTimer > data.attackTime[2])
        {
            ani.SetTrigger("����3 - Trigger");
            attackCount = 0;
            attackTimer = 0;
        }
        else if (attackCount == 1 && attackTimer > data.attackTime[1])
        {
            ani.SetTrigger("����2 - Trigger");
            attackCount += 1;
            attackTimer = 0;
        }
        else if (attackCount == 0 && attackTimer > data.attackTime[0])
        {
            ani.SetTrigger("����1 - Trigger");
            attackCount += 1;
            attackTimer = 0;
        }
    }

    /// <summary>
    /// ���m
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
    /// ��s��T
    /// </summary>
    void UpdateHUD()
    {
        hpBar.fillAmount = hp / data.hp;
        armorBar.fillAmount = armor / data.armor;
        rageBar.fillAmount = rage / data.rage;
    }
    #endregion
}
