using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class ShadowBoss : MonoBehaviour
{
    #region - Variables -
    [SerializeField] [Header("MoveSpeed")] float moveSpeed;
    [SerializeField] [Header("MoveXLimit")] Vector2 moveXLimit;

    [SerializeField] [Header("MaxHp")] float maxHp;
    [SerializeField] [Header("CurrentHp")] float currentHp;

    int attackCount;
    int skillCount;

    Animator ani;
    Rigidbody2D rig;
    GameObject player;
    #endregion

    #region - MonoBehaviour -
    void Awake()
    {
        currentHp = maxHp;
    }

    void Update()
    {
        Track();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player Weapon") Hurt(1);
    }
    #endregion

    #region - IEnumerator -
    /// <summary>
    /// Normal attack
    /// </summary>
    /// <returns>animation time</returns>
    IEnumerator NormalAttack()
    {
        ani.SetTrigger("Attack - Trigger");

        attackCount++;

        yield return new WaitForSeconds(2.917f);
    }

    /// <summary>
    /// Skill 01
    /// </summary>
    /// <returns>animation time</returns>
    IEnumerator Skill_01()
    {
        ani.SetTrigger("Skill 01 - Trigger");

        attackCount = 0;

        skillCount++;

        yield return new WaitForSeconds(2.917f);
    }

    /// <summary>
    /// Skill 2
    /// </summary>
    /// <returns>animation time</returns>
    IEnumerator Skill_02()
    {
        ani.SetTrigger("Skill 02 - Trigger");

        skillCount = 0;

        yield return new WaitForSeconds(6.250f);
    }
    #endregion

    #region - Methods -
    /// <summary>
    /// Track
    /// </summary>
    void Track()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(distance);

        if (distance <= 3f) Attack();
        else if (distance <= 8f) Move();
        else Idle();
    }

    /// <summary>
    /// Idle
    /// </summary>
    void Idle()
    {
        rig.velocity = Vector3.zero;

        ani.SetBool("Move - Bool", false);
    }

    /// <summary>
    /// Move
    /// </summary>
    void Move()
    {
        if (transform.position.x <= moveXLimit.x || transform.position.x >= moveXLimit.y) Idle();

        else
        {
            if ((transform.position.x - player.transform.position.x) < 0) rig.velocity = new Vector3(1, 1, 1) * Time.deltaTime * moveSpeed;

            else rig.velocity = new Vector3(-1, 1, 1) * Time.deltaTime * moveSpeed;

            ani.SetBool("Move - Bool", true);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, moveXLimit.x, moveXLimit.y), transform.position.y, 0);
        }
    }

    /// <summary>
    /// Hurt
    /// </summary>
    /// <param name="_damage">damage</param>
    void Hurt(int _damage)
    {
        currentHp -= _damage;

        ani.SetTrigger("Hurt - Trigger");

        if (currentHp <= 0) Die();
    }

    /// <summary>
    /// Die
    /// </summary>
    void Die()
    {
        this.enabled = false;

        ani.SetBool("Dead - Bool", true);
    }

    /// <summary>
    /// Attack mode
    /// </summary>
    void Attack()
    {
        rig.velocity = Vector3.zero;

        // attack style
        if (skillCount >= 2) StartCoroutine("Skill_02");
        else if (attackCount >= 4) StartCoroutine("Skill_01");
        else StartCoroutine("NormalAttack");

    }
    #endregion
}