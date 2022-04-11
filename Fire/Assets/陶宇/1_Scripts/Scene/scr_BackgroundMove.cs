using UnityEngine;

public class scr_BackgroundMove : MonoBehaviour
{
    [SerializeField] [Header("���a����")] GameObject player;

    [SerializeField] [Header("�i���ʪ��I��")] bool movable;
    [SerializeField] [Header("���ʳt��")] float speed;

    scr_PlayerController playerController;

    void Awake()
    {
        player = GameObject.Find("�N����_Player");
        playerController = player.GetComponent<scr_PlayerController>();
    }

    void Update()
    {
        Track(speed);
    }

    void Track(float distance)
    {
        Vector3 temp = transform.localPosition;
        Vector3 target = player.transform.localPosition;

        if (movable)
        {
            temp.x = Mathf.Clamp(temp.x, -31, 31);
            temp.y = 2.4f;
            temp.z = 0;

            if (playerController.holding_left)
            {
                temp.x -= distance * Time.deltaTime;
            }
            else if (playerController.holding_Right)
            {
                temp.x += distance * Time.deltaTime;
            }
        }
        else if (!movable)
        {
            target.x = Mathf.Clamp(target.x, -31, 31);
            target.y = 2.4f;
            target.z = 0;

            temp = target;
        }
        transform.localPosition = temp;
    }
}
