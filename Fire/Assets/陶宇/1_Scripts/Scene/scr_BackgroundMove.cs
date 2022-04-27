using UnityEngine;

public class scr_BackgroundMove : MonoBehaviour
{
    [SerializeField] [Header("�i���ʪ��I��")] bool movable;

    [SerializeField] [Header("���ʳt��")] float speed;

    [SerializeField] [Header("�ᴺ���k���� / �e�����ٲ�")] Vector2 backLimit;

    GameObject player;          // ���a����
    GameObject backObject;      // �ᴺ����
    scr_PlayerController playerController;

    void Awake()
    {
        player = GameObject.Find("�N����_Player");
        backObject = GameObject.Find("�I��_Sprites/�ᴺ");
        playerController = player.GetComponent<scr_PlayerController>();
    }

    void Start()
    {
        backLimit.x = -31f;
        backLimit.y = 31f;
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
        Vector3 target = player.transform.localPosition;

        // �e����
        if (movable)
        {
            if (backObject.transform.position.x == backLimit.x || backObject.transform.position.x == backLimit.y) return;

            else
            {
                temp.z = 0;

                if (playerController.holding_left) temp.x -= distance * Time.deltaTime;

                else if (playerController.holding_Right) temp.x += distance * Time.deltaTime;
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
