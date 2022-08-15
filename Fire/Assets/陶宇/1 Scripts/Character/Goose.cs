using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : scr_PlayerBase
{
    Transform rocketPos_1;
    Transform rocketPos_2;
    Transform rocketPos_3;
    [SerializeField] Transform[] rocketPos;

    [SerializeField] GameObject rocketObj;

    protected override void Start()
    {
        base.Start();

        rocketPos = new Transform[3];

        rocketPos[0] = gameObject.transform.GetChild(3).GetComponent<Transform>();
        rocketPos[1] = gameObject.transform.GetChild(4).GetComponent<Transform>();
        rocketPos[2] = gameObject.transform.GetChild(5).GetComponent<Transform>();
    }

    /// <summary>
    /// 判斷式
    /// </summary>
    protected override void Judgement()
    {
        if (jumpTimer >= 0.2f) isJumping = false;
        if (attackTimer >= playerdata.attackTime[0]) isAttacking = false;
        if (skillTimer <= 0) isSkilling = false;
        if (ultTimer <= 0) isUlt = false;

        ani.SetBool("技能 3 - Bool", isUlt);
    }

    /// <summary>
    /// 按鈕觸發
    /// </summary>
    protected override void ButtonOnclick()
    {
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);

        skill_1_btn.onClick.AddListener(() => { Skill("技能 1 - Trigger", playerdata.playerSkills[0].time, playerdata.playerSkills[0].cost); });
        skill_1_btn.onClick.AddListener(() => { StartCoroutine("Rush"); });
        skill_1_btn.onClick.AddListener(() => { StartCoroutine("Rush"); });

        skill_2_btn.onClick.AddListener(() => { Skill("技能 2 - Trigger", playerdata.playerSkills[1].time, playerdata.playerSkills[1].cost); });
        skill_2_btn.onClick.AddListener(() => { StartCoroutine("RocketAttack"); });

        skill_3_btn.onClick.AddListener(() => { Ultimate(playerdata.playerSkills[2].cost, playerdata.playerSkills[2].time, 8f); });
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    protected override void Attack()
    {
        if (isSkilling || isUlt) return;

        if (attackTimer > playerdata.attackTime[0])
        {
            ani.SetTrigger("攻擊 - Trigger");
            isAttacking = true;
            attackTimer = 0;
        }
    }

    /// <summary>
    /// 跳躍
    /// </summary>
    protected override void SetJump()
    {
        if (isUlt) return;

        isJumping = true;

        jumpTimer = 0;
    }

    /// <summary>
    /// 技能
    /// </summary>
    /// <param name="_name"> 技能名稱 </param>
    /// <param name="_time"> 技能CD </param>
    /// <param name="_cost"> 技能消耗 </param>
    protected override void Skill(string _name, float _time, int _cost)
    {
        if (isSkilling || rage < _cost) return;

        ani.SetTrigger(_name);
        skillTimer = _time;
        isSkilling = true;
    }


    /// <summary>
    /// 往前衝
    /// </summary>
    /// <returns></returns>
    IEnumerator Rush()
    {
        yield return new WaitForSeconds(1.2f);

        for (int i = 0; i < 20; i++)
        {
            transform.Translate(1, 0, 0);

            yield return new WaitForSeconds(0.025f);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, gameManager.playerxLimit.x, gameManager.playerxLimit.y), transform.position.y, 0);
        }
    }


    /// <summary>
    /// 射火箭
    /// </summary>
    IEnumerator RocketAttack()
    {
        yield return new WaitForSeconds(1.9f);

        for (int i = 0; i < rocketPos.Length; i++) Instantiate(rocketObj, rocketPos[i]);
    }

    /// <summary>
    /// 大招
    /// </summary>
    /// <param name="_cost"> 技能消耗 </param>
    /// <param name="_time"> 技能動畫時長 </param>
    /// <param name="_ultTime"> 大招持續時間 </param>
    void Ultimate(int _cost, float _time, float _ultTime)
    {
        if (rage < _cost) return;

        isUlt = true;

        skillTimer = _time;
        ultTimer = _ultTime;
    }
}
