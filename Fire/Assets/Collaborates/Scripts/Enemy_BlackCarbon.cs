using UnityEngine;

public class Enemy_BlackCarbon : MonoBehaviour
{
    bool movingRight;
    bool canMove;
    bool isDead;

    float speed;
    float moveTimer;      // ���ʭp�ɾ�
    float attackTimer;    // �C���������ɶ�
    float destoryTime;    // �R������
    float hp;             // �ͩR��

    int attackCount;      // �p�����������
    int skillCount;       // �p��ޯ�I�񦸼�

    Animator ani;
    Vector3 beginPos;
    scr_PlayerBase player;

    void Awake()
    {
        ani = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("���a").GetComponent<scr_PlayerBase>();
    }

    void Start()
    {
        InitValue();

        beginPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void InitValue()
    {
        speed = 3f;
        destoryTime = 1.2f;
        hp = 10;

        attackCount = 0;
        skillCount = 0;

        movingRight = true;
    }

    void Update()
    {
        Timer();
        StateCheck();

        Move();
    }

    void Timer()
    {
        if (!isDead)
        {
            moveTimer += Time.deltaTime;
            attackTimer += Time.deltaTime;
        }
    }

    /// <summary>
    /// �I��Ĳ�o
    /// </summary>
    /// <param name="col">�I����</param>
    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player Weapon")
        {
            Hurt(player.atk);
            player.rage += 0.5f;
        }
    }

    /// <summary>
    /// ���A��
    /// </summary>
    void StateCheck()
    {
        if (moveTimer >= 4) canMove = true;
        else canMove = false;
    }

    /// <summary>
    /// ����
    /// </summary>
    void Move()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (Mathf.Abs(distance) <= 3f)
        {
            moveTimer = 0;

            if (attackTimer > 1.283f) Attack();
        }

        if (canMove)
        {
            if (movingRight)
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                transform.localScale = new Vector3(-1, 1, 1);
                ani.SetBool("walk", true);

                if (transform.position.x - beginPos.x >= 5)
                {
                    moveTimer = 0;
                    movingRight = false;
                    ani.SetBool("walk", false);
                }
            }
            else
            {
                transform.Translate(Vector3.left * speed * Time.deltaTime);
                transform.localScale = new Vector3(1, 1, 1);
                ani.SetBool("walk", true);

                if (transform.position.x - beginPos.x <= -5)
                {
                    moveTimer = 0;
                    movingRight = true;
                    ani.SetBool("walk", false);
                }
            }
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    void Attack()
    {
        attackTimer = 0;
        skillCount++;

        if (skillCount >= 5)
        {
            ani.SetTrigger("skill");
            skillCount = 0;
        }
        else
        {
            moveTimer = 0;
            canMove = false;
            ani.SetBool("walk", false);
            ani.SetTrigger("attacksss");
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    void Hurt(float _damage)
    {
        hp -= _damage;
        moveTimer = 0;

        if (hp <= 0) Dead();

        else ani.SetTrigger("hurt");
    }

    /// <summary>
    /// ���`
    /// </summary>
    void Dead()
    {
        isDead = true;
        canMove = false;

        ani.SetBool("dead", isDead);
        Destroy(this.gameObject, destoryTime);
    }
}
