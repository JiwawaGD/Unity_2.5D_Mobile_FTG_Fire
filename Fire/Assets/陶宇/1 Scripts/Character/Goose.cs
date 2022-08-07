using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : scr_PlayerBase
{
    /// <summary>
    /// 判斷式
    /// </summary>
    protected override void Judgement()
    {
        if (jumpTimer >= 0.2f) isJumping = false;
        if (skillTimer <= 0) isSkilling = false;
        if (attackTimer >= attackInterval) attackCount = 0;
        if (ultTimer <= 0) isUlt = false;

        ani.SetBool("技能 3 - Bool", isUlt);
    }

    /// <summary>
    /// 按鈕觸發
    /// </summary>
    protected override void ButtonOnclick()
    {
        Debug.Log("here");
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);

        skill_1_btn.onClick.AddListener(() => { Skill("技能 1 - Trigger", playerdata.playerSkills[0].time, playerdata.playerSkills[0].cost); });
        skill_2_btn.onClick.AddListener(() => { Skill("技能 2 - Trigger", playerdata.playerSkills[1].time, playerdata.playerSkills[1].cost); });
        skill_3_btn.onClick.AddListener(() => { Ultimate(playerdata.playerSkills[2].cost, playerdata.playerSkills[2].time, 8f); });
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    protected override void Attack()
    {
        if (isSkilling) return;

        if (attackTimer > playerdata.attackTime[0])
        {
            ani.SetTrigger("攻擊 - Trigger");
            attackTimer = 0;
        }
    }

    /// <summary>
    /// 技能
    /// </summary>
    /// <param name="_name">技能名稱</param>
    /// <param name="_time">技能CD</param>
    /// <param name="_cost">技能消耗</param>
    protected override void Skill(string _name, float _time, int _cost)
    {
        if (rage < _cost) return;

        ani.SetTrigger(_name);
        skillTimer = _time;
        isSkilling = true;
    }

    /// <summary>
    /// 大招
    /// </summary>
    /// <param name="_cost">技能消耗</param>
    /// <param name="_time">技能動畫時長</param>
    /// <param name="_ultTime">大招持續時間</param>
    void Ultimate(int _cost, float _time, float _ultTime)
    {
        if (rage < _cost) return;

        isSkilling = true;
        isUlt = true;

        skillTimer = _time;
        ultTimer = _ultTime;
    }
}
