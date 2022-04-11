using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerController : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("������")] scr_PlayerData data;

    [SerializeField] [Header("�a�ߤޤO")] float gravity;

    float moveSpeed;             // ���ʳt��
    float jumpHeight;            // ���D���׭���
    float jumpForce;             // ���D�O�D
    float hp;                    // �ͩR��

    public bool holding_left;   // ���U����
    public bool holding_Right;  // ���U����

    Button jump_btn;             // ���D - ���s
    Vector3 moveDir;             // ���ʮy��
    Rigidbody rig;               // ����
    Animator ani;                // �ʵe

    [HideInInspector] [Header("�O�_�b�a�W")] public bool isGrounded;
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        jump_btn = GameObject.Find("��_btn").GetComponent<Button>();
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
    /// �Ҧ��P���馳������
    /// </summary>
    void Movement()
    {
        // �W�[�U�Y�t��
        if (transform.position.y >= jumpHeight) rig.velocity -= new Vector3(0, gravity * Time.deltaTime, 0);

        // ����
        if (!holding_left && !holding_Right) Idle();

        // ����
        if (holding_left || Input.GetKey(KeyCode.A)) Move(new Vector3(-1, 0, 0), new Vector3(-1, 1, 1));
        if (holding_Right || Input.GetKey(KeyCode.D)) Move(new Vector3(1, 0, 0), new Vector3(1, 1, 1));

        // ���D
        jump_btn.onClick.AddListener(Jump);

        // ���եΥN�X
        if (Input.GetKey(KeyCode.Space)) Jump();
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
    /// ���D
    /// </summary>
    void Jump()
    {
        if (isGrounded) rig.velocity = new Vector3(0, jumpForce * Time.deltaTime * 60f, 0);

        else return;
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

    }
    #endregion
}
