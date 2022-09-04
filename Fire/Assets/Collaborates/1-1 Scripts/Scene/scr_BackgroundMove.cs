using UnityEngine;

public class scr_BackgroundMove : MonoBehaviour
{
    [SerializeField] [Header("�i���ʪ��I��")] bool movable;

    [SerializeField] [Header("���ʳt��")] float speed;

    [SerializeField] [Header("�ᴺ���k���� / �e�����ٲ�")] Vector2 backLimit;

    GameObject backObject;      // �ᴺ����
    scr_PlayerBase playerBase;
    scr_GameManager gameManager;

    void Awake()
    {
        playerBase = FindObjectOfType<scr_PlayerBase>();
        gameManager = FindObjectOfType<scr_GameManager>();
        backObject = GameObject.Find("�I��/�ᴺ");
    }

    void Start()
    {
        backLimit.x = -31f;
        backLimit.y = 41f;
    }

    void Update()
    {
        Track(speed);
    }

    /// <summary>
    /// �I������
    /// </summary>
    /// <param name="distance">���ʶq</param>
    void Track(float distance)
    {
        Vector3 temp = transform.localPosition;
        Vector3 target = playerBase.transform.localPosition;

        if (playerBase.isDead) return;

        else
        {
            // �e����
            if (movable)
            {
                if (backObject.transform.position.x == backLimit.x || backObject.transform.position.x == backLimit.y) return;

                else
                {
                    temp.z = 0;

                    if (gameManager.holding_left) temp.x -= distance * Time.deltaTime;

                    else if (gameManager.holding_Right) temp.x += distance * Time.deltaTime;
                }
            }
            // �ᴺ
            else if (!movable)
            {   
                target.x = Mathf.Clamp(target.x, backLimit.x, backLimit.y);
                target.y = 2.4f;
                target.z = 0;

                temp = target;
            }
            transform.localPosition = temp;
        }
    }
}
