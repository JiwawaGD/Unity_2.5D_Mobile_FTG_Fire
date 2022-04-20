using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerController : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("������")] scr_PlayerData data;

    [SerializeField] [Header("�a�ߤޤO")] float gravity;

    [SerializeField] [Header("�������j")] float attackInterval;

    float hp;                    // �ͩR��
    float moveSpeed;             // ���ʳt��
    float jumpHeight;            // ���D���׭���
    float jumpForce;             // ���D�O�D

    float jumpTimer;             // ���D�p�ɾ�
    float attackTimer;           // �����p�ɾ�
    int attackCount;             // �����p�ƾ�
    bool isJumping;              // �O�_���D

    Button jump_btn;             // ���D - ���s
    Button attack_btn;           // ���� - ���s
    Vector3 moveDir;             // ���ʮy��
    Rigidbody rig;               // ����
    Animator ani;                // �ʵe

    [HideInInspector] public bool holding_left;      // ���U����
    [HideInInspector] public bool holding_Right;     // ���U����
    [HideInInspector] public bool isGrounded;        // �O�_�b�a�W
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();

        jump_btn = GameObject.Find("��_btn").GetComponent<Button>();
        attack_btn = GameObject.Find("��_btn").GetComponent<Button>();
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

        attackCount = 0;

        // ���s�ƥ�
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);
    }

    void Update()
    {
        Movement();

        // �p�ɾ�
        jumpTimer += Time.deltaTime;
        attackTimer += Time.deltaTime;

        // ���L��
        if (jumpTimer >= 0.5f) isJumping = false;
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
    /// ����
    /// </summary>
    void Idle()
    {
        moveDir = Vector3.zero;

        ani.SetBool("���� - Bool", false);
    }

    /// <summary>
    /// ���ʥ\��
    /// </summary>
    /// <param name="direction">���ʮy��</param>
    /// <param name="scale">����ؤo</param>
    void Move(Vector3 direction, Vector3 scale)
    {
        moveDir = direction;

        rig.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

        ani.SetBool("���� - Bool", true);

        transform.localScale = scale;
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
    /// ����
    /// </summary>
    /// <param name="damage">�ˮ`��</param>
    void Hurt(float damage)
    {
        hp -= damage;

        if (hp > 0) ani.SetTrigger("���� - Trigger");

        else if (hp <= 0) Die();

        Debug.Log(hp);
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
        if (attackCount == 2 && attackTimer > 0.9f)
        {
            print("final attack");

            attackCount = 0;
        }
        else if (attackCount == 1 && attackTimer > 0.8f)
        {
            attackTimer = 0;

            ani.SetTrigger("����2 - Trigger");

            attackCount += 1;
        }
        else if (attackCount == 0)
        {
            attackTimer = 0;

            ani.SetTrigger("����1 - Trigger");

            attackCount += 1;
        }
    }
    #endregion
}
