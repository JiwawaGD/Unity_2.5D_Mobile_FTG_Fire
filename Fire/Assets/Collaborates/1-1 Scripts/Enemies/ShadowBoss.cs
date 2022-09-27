using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator))]
public class ShadowBoss : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("MoveSpeed")] float moveSpeed;
    [SerializeField] [Header("MoveXLimit")] Vector2 moveXLimit;

    [SerializeField] [Header("MaxHp")] float maxHp;
    [SerializeField] [Header("CurrentHp")] float currentHp;

    int attackCount;
    int skillCount;

    bool hasAttack = true;
    bool hasDie = false;

    Animator ani;
    Rigidbody rig;
    GameObject player;
    SpriteRenderer sr;
    Collider coll;
    #endregion

    #region - MonoBehaviour -
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("ª±®a");
        rig = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider>();
        currentHp = maxHp;
    }

    void FixedUpdate()
    {
        Track();
    }

    void OnTriggerEnter(Collider col)
    {
        if (hasDie)
            return;

        if (col.gameObject.CompareTag("Player Weapon"))
            Hurt(1);
    }
    #endregion

    #region - IEnumerator -
    /// <summary>
    /// Normal attack
    /// </summary>
    /// <returns>animation time</returns>
    IEnumerator NormalAttack()
    {
        hasAttack = false;

        ani.SetTrigger("Attack - Trigger");

        attackCount++;

        yield return new WaitForSeconds(3.5f);

        hasAttack = true;
    }

    /// <summary>
    /// Skill 01
    /// </summary>
    /// <returns>animation time</returns>
    IEnumerator Skill_01()
    {
        hasAttack = false;

        ani.SetTrigger("Skill 01 - Trigger");

        attackCount = 0;

        skillCount++;

        yield return new WaitForSeconds(3.5f);

        hasAttack = true;
    }

    /// <summary>
    /// Skill 2
    /// </summary>
    /// <returns>animation time</returns>
    IEnumerator Skill_02()
    {
        hasAttack = false;

        ani.SetTrigger("Skill 02 - Trigger");

        skillCount = 0;

        yield return new WaitForSeconds(7f);

        hasAttack = true;
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// Track
    /// </summary>
    void Track()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance <= 10f && hasAttack)
            Attack();
        else if (distance <= 30f && hasAttack)
            Move();
        else if (distance >= 30f)
            Idle();
    }

    /// <summary>
    /// Idle
    /// </summary>
    void Idle()
    {
        rig.velocity = Vector3.zero;
    }

    /// <summary>
    /// Move
    /// </summary>
    void Move()
    {
        if ((transform.position.x - player.transform.position.x) < 0)
            rig.velocity = new Vector3(1, 0, 0) * Time.deltaTime * moveSpeed;
        else
            rig.velocity = new Vector3(-1, 0, 0) * Time.deltaTime * moveSpeed;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, moveXLimit.x, moveXLimit.y), transform.position.y, 0);
    }

    /// <summary>
    /// Hurt
    /// </summary>
    /// <param name="_damage">damage</param>
    void Hurt(int _damage)
    {
        currentHp -= _damage;

        ani.SetTrigger("Hurt - Trigger");

        if (currentHp <= 0)
            Die();
    }

    /// <summary>
    /// Die
    /// </summary>
    void Die()
    {
        ani.SetBool("Dead - Bool", true);

        hasDie = true;
        coll.enabled = false;
        rig.useGravity = false;

        this.enabled = false;
    }

    /// <summary>
    /// Attack mode
    /// </summary>
    void Attack()
    {
        rig.velocity = new Vector3(0, 0, 0);

        if ((transform.position.x - player.transform.position.x) < 0)
            sr.flipX = true;
        else
            sr.flipX = false;

        // attack style
        if (skillCount >= 2)
            StartCoroutine("Skill_02");
        else if (attackCount >= 4)
            StartCoroutine("Skill_01");
        else
            StartCoroutine("NormalAttack");
    }
    #endregion
}