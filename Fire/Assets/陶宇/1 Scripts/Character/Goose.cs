using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : scr_PlayerBase
{
    [SerializeField] Transform[] rocketPos;
    [SerializeField] Transform babyGoosePos;

    [SerializeField] GameObject rocketObj;
    [SerializeField] GameObject babyGooseObj;
    BabyGoose babyGoose;

    int attackAniID;
    bool isSkill_1;
    bool isSkill_2;

    float babyGooseSpawnTimer;


    protected override void Start()
    {
        base.Start();

        attackAniID = Animator.StringToHash("���� - Trigger");

        rocketPos = new Transform[3];

        babyGoose = babyGooseObj.GetComponent<BabyGoose>();

        rocketPos[0] = gameObject.transform.GetChild(3).GetComponent<Transform>();
        rocketPos[1] = gameObject.transform.GetChild(4).GetComponent<Transform>();
        rocketPos[2] = gameObject.transform.GetChild(5).GetComponent<Transform>();
        babyGoosePos = gameObject.transform.GetChild(6).GetComponent<Transform>();
    }

    /// <summary>
    /// �p�ɾ�
    /// </summary>
    protected override void Timer()
    {
        base.Timer();

        if (babyGooseSpawnTimer <= 2) babyGooseSpawnTimer += Time.deltaTime;
    }

    /// <summary>
    /// �P�_��
    /// </summary>
    protected override void Judgement()
    {
        if (jumpTimer >= 0.2f) isJumping = false;
        if (attackTimer >= playerdata.attackTime[0]) isAttacking = false;
        if (skillTimer <= 0) isSkilling = false;
        if (ultTimer <= 0) isUlt = false;

        if (hurtTimer >= 6f && armor <= playerdata.armorMax) armor += 1 * Time.deltaTime;

        SpawnBabyGoose(isUlt,faceRight);

        ani.SetBool("�ޯ� 3 - Bool", isUlt);
    }

    /// <summary>
    /// ���sĲ�o
    /// </summary>
    protected override void ButtonOnclick()
    {
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);

        skill_1_btn.onClick.AddListener(() => { Skill("�ޯ� 1 - Trigger", playerdata.playerSkills[0].time, playerdata.playerSkills[0].cost); });
        skill_1_btn.onClick.AddListener(() => { StartCoroutine("Rush", playerdata.playerSkills[0].cost); });

        skill_2_btn.onClick.AddListener(() => { Skill("�ޯ� 2 - Trigger", playerdata.playerSkills[1].time, playerdata.playerSkills[1].cost); });
        skill_2_btn.onClick.AddListener(() => { StartCoroutine("RocketAttack", playerdata.playerSkills[1].cost); });

        skill_3_btn.onClick.AddListener(() => { Ultimate(playerdata.playerSkills[2].cost, 6f, 6f); });
    }

    /// <summary>
    /// ����
    /// </summary>
    protected override void Attack()
    {
        if (isSkilling || isUlt) return;

        if (attackTimer > playerdata.attackTime[0])
        {
            ani.SetTrigger(attackAniID);
            isAttacking = true;
            attackTimer = 0;
        }
    }

    /// <summary>
    /// ���D
    /// </summary>
    protected override void SetJump()
    {
        if (isUlt) return;

        isJumping = true;

        jumpTimer = 0;
    }

    /// <summary>
    /// �ޯ�
    /// </summary>
    /// <param name="_name"> �ޯ�W�� </param>
    /// <param name="_time"> �ޯ�CD </param>
    /// <param name="_cost"> �ޯ���� </param>
    protected override void Skill(string _name, float _time, int _cost)
    {
        if (isSkilling || rage < _cost) return;

        rage -= _cost;
        ani.SetTrigger(_name);
        skillTimer = _time;
        isSkilling = true;

        isSkill_1 = true;
        isSkill_2 = true;
    }

    /// <summary>
    /// ���e��
    /// </summary>
    IEnumerator Rush(int _cost)
    {
        if (isUlt) yield break;

        if (isSkill_1)
        {
            yield return new WaitForSeconds(1.2f);

            for (int i = 0; i < 20; i++)
            {
                transform.Translate(1, 0, 0);

                yield return new WaitForSeconds(0.025f);

                transform.position = new Vector3(Mathf.Clamp(transform.position.x, gameManager.playerxLimit.x, gameManager.playerxLimit.y), transform.position.y, 0);
            }
        }

        isSkill_1 = false;
        isSkill_2 = false;
    }

    /// <summary>
    /// �g���b
    /// </summary>
    IEnumerator RocketAttack(int _cost)
    {
        if (isUlt) yield break;

        if (isSkill_2)
        {
            yield return new WaitForSeconds(1.9f);

            for (int i = 0; i < rocketPos.Length; i++) Instantiate(rocketObj, rocketPos[i]);
        }

        isSkill_1 = false;
        isSkill_2 = false;
    }

    /// <summary>
    /// �j��
    /// </summary>
    /// <param name="_cost"> �ޯ���� </param>
    /// <param name="_time"> �ޯ�ʵe�ɪ� </param>
    /// <param name="_ultTime"> �j�۫���ɶ� </param>
    void Ultimate(int _cost, float _time, float _ultTime)
    {
        if (rage < _cost) return;

        rage -= _cost;

        isUlt = true;

        skillTimer = _time;
        ultTimer = _ultTime;
    }

    /// <summary>
    /// �ͦ��p�Z
    /// </summary>
    /// <param name="_isUlt">�O�_�j�ۤ�</param>
    void SpawnBabyGoose(bool _isUlt, bool _faceRight)
    {
        if (_isUlt)
        {
            if (babyGooseSpawnTimer > 0.6f)
            {
                babyGoose.faceRight = _faceRight;
                Instantiate(babyGooseObj, babyGoosePos);
                babyGooseSpawnTimer = 0;
            }
        }
    }
}
