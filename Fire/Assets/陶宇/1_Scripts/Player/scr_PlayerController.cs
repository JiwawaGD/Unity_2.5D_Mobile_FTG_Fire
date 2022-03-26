using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerController : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("���ʳt��")] float moveSpeed;
    [SerializeField] [Header("���D�O�D")] float jumpForce;
    [SerializeField] [Header("���D���׭���")] float jumpHeight;
    [SerializeField] [Header("�a�ߤޤO")] float gravity;

    [SerializeField] [Header("���D - ���s")] Button jump_btn;

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
        jump_btn = GameObject.Find("��_btn").GetComponent<Button>();
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
    /// �Ҧ��P���馳������
    /// </summary>
    void Movement()
    {
        // ����
        if (holding_left) Move(new Vector3(-2, 0, 0), new Vector3(-1f, 1, 1));

        if (holding_Right) Move(new Vector3(2, 0, 0), new Vector3(1, 1, 1));

        if (!holding_left && !holding_Right) moveDir = Vector3.zero;

        // ���D
        jump_btn.onClick.AddListener(Jump);

        // �W�[�U�Y�t��
        if (transform.position.y >= jumpHeight) rig.velocity -= new Vector3(0, gravity * Time.deltaTime, 0);
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

        transform.localScale = scale;
    }

    /// <summary>
    /// ���D
    /// </summary>
    void Jump()
    {
        if (isGrounded) rig.velocity = new Vector3(0, jumpForce, 0);
        else return;
    }
    #endregion
}
