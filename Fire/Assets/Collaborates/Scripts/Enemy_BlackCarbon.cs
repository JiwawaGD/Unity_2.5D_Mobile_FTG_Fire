using UnityEngine;

public class Enemy_BlackCarbon : MonoBehaviour
{
    string playerWeapon;
    string playerWeaponCollider;
    string rocket;

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
    BoxCollider boxCollider;
    Rigidbody rigidbody;
    Vector3 beginPos;
    scr_PlayerBase player;
    scr_GameManager gameManager;

    void Awake()
    {
        ani = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("���a").GetComponent<scr_PlayerBase>();
        gameManager = GameObject.Find("GameManager").GetComponent<scr_GameManager>();
    }

    void Start()
    {
        InitValue();

        beginPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    void InitValue()
    {
        playerWeapon = "Player Weapon";
        playerWeaponCollider = "Player Weapon Collider";
        rocket = "���b";

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

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == playerWeapon)
        {
            if (col.gameObject.name == playerWeaponCollider)
            {
                Hurt(player.atk);
                player.rage += 0.5f;
            }
            else if (col.gameObject.name.Contains(rocket))
            {
                Hurt(player.playerdata.playerSkills[1].damage);
                Destroy(col.gameObject);
            }
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
        bool onLeft;
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (Mathf.Abs(distance) <= 5f)
        {
            if (distance <= 0) onLeft = true;
            else onLeft = false;

            transform.localScale = onLeft ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

            moveTimer = 0;

            if (attackTimer > 1.8f) Attack();
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

        ani.SetBool("walk", false);

        if (hp <= 0) Dead();

        else ani.SetTrigger("hurt");
    }

    /// <summary>
    /// ���`
    /// </summary>
    void Dead()
    {
        boxCollider.enabled = false;
        rigidbody.useGravity = false;

        isDead = true;
        canMove = false;

        gameManager.killAmount += 1;

        ani.SetBool("dead", isDead);
        Destroy(this.gameObject, destoryTime);
    }
}
