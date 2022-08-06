using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goose : scr_PlayerBase
{
    /// <summary>
    /// ���sĲ�o
    /// </summary>
    protected override void ButtonOnclick()
    {
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);

        skill_1_btn.onClick.AddListener(() => { Skill("�ޯ� 1 - Trigger", playerdata.playerSkills[0].time, playerdata.playerSkills[0].cost); });
        skill_2_btn.onClick.AddListener(() => { Skill("�ޯ� 2 - Trigger", playerdata.playerSkills[1].time, playerdata.playerSkills[1].cost); });

        skill_3_btn.onClick.AddListener(() => { });
    }

    /// <summary>
    /// �ޯ�
    /// </summary>
    /// <param name="_name">�ޯ�W��</param>
    /// <param name="_time">�ޯ�CD</param>
    /// <param name="_cost">�ޯ����</param>
    protected override void Skill(string _name, float _time, int _cost)
    {
        if (rage < _cost) return;

        isSkilling = true;
        ani.SetTrigger(_name);
        skillTimer = _time;
    }

    void SkillDance(string _name,float _time)
    {
        ani.SetBool("", true);
    }

}
