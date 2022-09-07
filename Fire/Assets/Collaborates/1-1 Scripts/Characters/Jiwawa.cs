using UnityEngine;

public class Jiwawa : scr_PlayerBase
{
    /// <summary>
    /// �P�_��
    /// </summary>
    protected override void Judgement()
    {
        if (jumpTimer >= 0.2f) isJumping = false;
        if (skillTimer <= 0) isSkilling = false;
        if (attackTimer >= attackInterval) attackCount = 0;
        if (ultTimer <= 0) isUlt = false;
        if (hurtTimer >= 6f && armor <= playerdata.armorMax) armor += 1 * Time.deltaTime;

        ani.SetBool("�ޯ�3 - ���A - Bool", isUlt);
    }

    /// <summary>
    /// ���sĲ�o
    /// </summary>
    protected override void ButtonOnclick()
    {
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);

        skill_1_btn.onClick.AddListener(() => { Skill("�ޯ�1 - Trigger", playerdata.playerSkills[0].time, playerdata.playerSkills[0].cost); });
        skill_2_btn.onClick.AddListener(() => { Skill("�ޯ�2 - Trigger", playerdata.playerSkills[1].time, playerdata.playerSkills[1].cost); });
        skill_3_btn.onClick.AddListener(() => { Skill("�ޯ�3 - Trigger", playerdata.playerSkills[2].time, playerdata.playerSkills[2].cost); });
        skill_3_btn.onClick.AddListener(() => { Ultimate(playerdata.playerSkills[0].cost); });
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="direction">��V</param>
    /// <param name="scale">�ؤo</param>
    protected override void Move(Vector3 direction, Vector3 scale, bool _faceRight)
    {
        if (gameManager.holding_Defense || isSkilling) return;

        else
        {
            moveDir = direction;

            transform.Translate(moveDir * moveSpeed * Time.deltaTime);

            if (isUlt) ani.SetBool("�ޯ�3 - ���� - Bool", true);

            else ani.SetBool("���� - Bool", true);

            transform.localScale = scale;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, gameManager.playerxLimit.x, gameManager.playerxLimit.y), transform.position.y, 0);
        faceRight = _faceRight;
    }

    /// <summary>
    /// �ޯ�
    /// </summary>
    /// <param name="_name">�ޯ�W��</param>
    /// <param name="_time">�ޯ�CD</param>
    /// <param name="_cost">�ޯ����</param>
    protected override void Skill(string _name, float _time, int _cost)
    {
        if (isUlt || isSkilling) return;
        if (rage < _cost) return;

        isSkilling = true;
        ani.SetTrigger(_name);
        skillTimer = _time;
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="damage">�ˮ`��</param>
    protected override void Hurt(int damage)
    {
        if (isUlt) return;

        // ���m
        if (gameManager.holding_Defense && armor >= 0) armor -= damage;

        // �}�� or �����m
        else
        {
            if (hp > 0) ani.SetTrigger("���� - Trigger");

            hp -= damage;
        }

        hurtTimer = 0;

        // ���`
        if (hp <= 0) Die();
    }

    /// <summary>
    /// ����
    /// </summary>
    protected override void Idle()
    {
        moveDir = Vector3.zero;

        ani.SetBool("���� - Bool", false);

        ani.SetBool("�ޯ�3 - ���� - Bool", false);
    }

    /// <summary>
    /// ����
    /// </summary>
    protected override void Attack()
    {
        if (isSkilling) return;

        if (isUlt && attackTimer > 1.417f)
        {
            ani.SetTrigger("�ޯ�3 - ���� - Trigger");
            attackTimer = 0;
        }

        if (attackCount == 2 && attackTimer > playerdata.attackTime[2])
        {
            ani.SetTrigger("����3 - Trigger");
            attackCount = 0;
            attackTimer = 0;
        }
        else if (attackCount == 1 && attackTimer > playerdata.attackTime[1])
        {
            ani.SetTrigger("����2 - Trigger");
            attackCount += 1;
            attackTimer = 0;
        }
        else if (attackCount == 0 && attackTimer > playerdata.attackTime[0])
        {
            ani.SetTrigger("����1 - Trigger");
            attackCount += 1;
            attackTimer = 0;
        }
    }

    /// <summary>
    /// ���m
    /// </summary>
    protected override void Defense()
    {
        if (isSkilling || isUlt) return;

        ani.SetBool("���m - Bool", gameManager.holding_Defense);

        if (hurtTimer >= 6f && armor <= playerdata.armor) armor += 5;
    }

    /// <summary>
    /// �ϥΤj��
    /// </summary>
    void Ultimate(int _cost)
    {
        if (rage < _cost) return;

        isUlt = true;

        ultTimer = 10f;
    }
}
