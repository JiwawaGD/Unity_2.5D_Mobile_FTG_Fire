using UnityEngine;
using System.Collections;

public class Enemy_BlackCarbon : MonoBehaviour
{
    string playerWeapon;
    string playerWeaponCollider;
    string rocket;
    string babyGoose;

    bool movingRight;
    bool canMove;
    bool isDead;
    bool hitByBabyGoose;

    float speed;
    float moveTimer;      // 移動計時器
    float attackTimer;    // 每次攻擊的時間
    float destoryTime;    // 刪除延遲
    float hp;             // 生命值
    float hitTimer;

    int skillCount;       // 計算技能施放次數

    Animator ani;
    BoxCollider boxCollider;
    Rigidbody rig;
    Vector3 beginPos;
    scr_PlayerBase player;
    scr_GameManager gameManager;

    void Awake()
    {
        ani = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        rig = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("玩家").GetComponent<scr_PlayerBase>();
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
        rocket = "火箭";
        babyGoose = "小鵝";

        speed = 3f;
        destoryTime = 1.2f;
        hp = 15;

        skillCount = 0;

        movingRight = true;
    }

    void Update()
    {
        Timer();
        StateCheck();
        Move();
        Judgement();
    }

    void Timer()
    {
        if (!isDead)
        {
            if (!hitByBabyGoose) moveTimer += Time.deltaTime;

            if (attackTimer <= 2.5f) attackTimer += Time.deltaTime;

            if (hitTimer <= 4f) hitTimer += Time.deltaTime;

            if (hitTimer >= 3f) hitByBabyGoose = false;
        }
    }

    void Judgement()
    {
        if (hitByBabyGoose)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            canMove = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            canMove = true;
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
            else if (col.gameObject.name.Contains(babyGoose))
            {
                Hurt(player.playerdata.playerSkills[2].damage);
                hitTimer = 0;
                moveTimer = 0;
                hitByBabyGoose = true;
            }
        }
    }

    /// <summary>
    /// 狀態機
    /// </summary>
    void StateCheck()
    {
        if (moveTimer >= 4) canMove = true;
        else canMove = false;
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Move()
    {
        if (canMove)
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
    /// 攻擊
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
    /// 受傷
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
    /// 死亡
    /// </summary>
    void Dead()
    {
        boxCollider.enabled = false;
        rig.useGravity = false;

        isDead = true;
        canMove = false;

        gameManager.killAmount += 1;

        ani.SetBool("dead", isDead);
        Destroy(this.gameObject, destoryTime);
    }
}
