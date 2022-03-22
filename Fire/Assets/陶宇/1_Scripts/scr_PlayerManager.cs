using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerManager : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("���ʳt��")] float moveSpeed;

    bool holding_left = false;
    bool holding_Right = false;

    Vector3 moveDir;
    Rigidbody rig;
    #endregion

    #region - Monobehaviour -
    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        holding_left = false;
        holding_Right = false;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        Movement();
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// ���ʧP�_��
    /// </summary>
    /// <param name="value">���ʶq</param>
    public void Movement()
    {
        if (holding_left) Move(new Vector3(-2, 0, 0), new Vector3(-0.5f, 0.5f, 0.5f));

        if (holding_Right) Move(new Vector3(2, 0, 0), new Vector3(0.5f, 0.5f, 0.5f));

        if (!holding_left && !holding_Right) moveDir = Vector3.zero;

    }

    /// <summary>
    /// ���ʥ\��
    /// </summary>
    /// <param name="direction">���ʮy��</param>
    /// <param name="scale">����ؤo</param>
    public void Move(Vector3 direction, Vector3 scale)
    {
        moveDir = direction;

        rig.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

        transform.localScale = scale;
    }

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
}
