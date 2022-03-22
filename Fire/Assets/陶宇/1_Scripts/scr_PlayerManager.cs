using UnityEngine;
using UnityEngine.UI;

public class scr_PlayerManager : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("移動速度")] float moveSpeed;

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
    /// 移動判斷式
    /// </summary>
    /// <param name="value">移動量</param>
    public void Movement()
    {
        if (holding_left) Move(new Vector3(-2, 0, 0), new Vector3(-0.5f, 0.5f, 0.5f));

        if (holding_Right) Move(new Vector3(2, 0, 0), new Vector3(0.5f, 0.5f, 0.5f));

        if (!holding_left && !holding_Right) moveDir = Vector3.zero;

    }

    /// <summary>
    /// 移動功能
    /// </summary>
    /// <param name="direction">移動座標</param>
    /// <param name="scale">物件尺寸</param>
    public void Move(Vector3 direction, Vector3 scale)
    {
        moveDir = direction;

        rig.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);

        transform.localScale = scale;
    }

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
    #endregion
}
