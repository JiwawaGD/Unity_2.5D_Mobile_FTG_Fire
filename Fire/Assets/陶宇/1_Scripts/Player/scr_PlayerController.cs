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

    int hp;                      // �ͩR��
    int armor;                   // �@�ҭ�
    int attackCount;             // �����p�ƾ�

    bool isJumping;              // �O�_���D

    Button jump_btn;             // ���D - ���s
    Button attack_btn;           // ���� - ���s
    Button skill_1_btn;          // �ޯ�1 - ���s
    Vector3 moveDir;             // ���ʮy��
    Rigidbody rig;               // ����
    Animator ani;                // �ʵe

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

        jump_btn = GameObject.Find("��__btn").GetComponent<Button>();
        attack_btn = GameObject.Find("��__btn").GetComponent<Button>();
        skill_1_btn = GameObject.Find("�ޯ�1__Btn").GetComponent<Button>();
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

        // ���s
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);
        skill_1_btn.onClick.AddListener(() => { Skill("�ޯ�1 - Trigger", 1.667f); });
    }

    void Update()
    {
        Movement();

        // �p�ɾ�
        jumpTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;
        skillTimer += Time.deltaTime;
        hurtTimer += Time.deltaTime;

        // �P�_��
        if (jumpTimer >= 0.2f) isJumping = false;
        if (attackTimer >= attackInterval) attackCount = 0;
    }

    void FixedUpdate()
    {
        Movement_Rig();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "�i�Ϫ��a����") Hurt(1);
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
    }
    #endregion

    #region - Methods -
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

    /// <summary>
    /// ���ʥ\��
    /// </summary>
    /// <param name="direction">���ʮy��</param>
    /// <param name="scale">����ؤo</param>
    void Move(Vector3 direction, Vector3 scale)
    {
        // ���m�����i��
        if (isDefense) return;

        else
        {
            moveDir = direction;

            rig.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

            ani.SetBool("���� - Bool", true);

            transform.localScale = scale;
        }
    }

    /// <summary>
    /// �I��ޯ�
    /// </summary>
    /// <param name="skillname">�ޯ�W��</param>
    /// <param name="skilltime">�ޯ�ɶ�</param>
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
    /// ����
    /// </summary>
    /// <param name="damage">�ˮ`��</param>
    void Hurt(int damage)
    {
        // ���m
        if (isDefense && armor >= 0)
        {
            armor -= damage;
        }
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
        ani.SetBool("���` - Bool", true);

        this.enabled = false;
    }

    /// <summary>
    /// ����
    /// </summary>
    void Attack()
    {
        if (attackCount == 2 && attackTimer > 0.78f)
        {
            ani.SetTrigger("����3 - Trigger");

            attackTimer = 0;
            attackCount = 0;
        }
        else if (attackCount == 1 && attackTimer > 0.68f)
        {
            ani.SetTrigger("����2 - Trigger");

            attackTimer = 0;
            attackCount += 1;
        }
        else if (attackCount == 0 && attackTimer > 0.68f)
        {
            ani.SetTrigger("����1 - Trigger");

            attackTimer = 0;
            attackCount += 1;
        }
    }

    /// <summary>
    /// ���m
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
