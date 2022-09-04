using UnityEngine;

public class Jiwawa : scr_PlayerBase
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
        if (hurtTimer >= 6f && armor <= playerdata.armorMax) armor += 1 * Time.deltaTime;

        ani.SetBool("技能3 - 狀態 - Bool", isUlt);
    }

    /// <summary>
    /// 按鈕觸發
    /// </summary>
    protected override void ButtonOnclick()
    {
        attack_btn.onClick.AddListener(Attack);
        jump_btn.onClick.AddListener(SetJump);

        skill_1_btn.onClick.AddListener(() => { Skill("技能1 - Trigger", playerdata.playerSkills[0].time, playerdata.playerSkills[0].cost); });
        skill_2_btn.onClick.AddListener(() => { Skill("技能2 - Trigger", playerdata.playerSkills[1].time, playerdata.playerSkills[1].cost); });
        skill_3_btn.onClick.AddListener(() => { Skill("技能3 - Trigger", playerdata.playerSkills[2].time, playerdata.playerSkills[2].cost); });
        skill_3_btn.onClick.AddListener(() => { Ultimate(playerdata.playerSkills[0].cost); });
    }

    /// <summary>
    /// 移動
    /// </summary>
    /// <param name="direction">方向</param>
    /// <param name="scale">尺寸</param>
    protected override void Move(Vector3 direction, Vector3 scale, bool _faceRight)
    {
        if (gameManager.holding_Defense || isSkilling) return;

        else
        {
            moveDir = direction;

            transform.Translate(moveDir * moveSpeed * Time.deltaTime);

            if (isUlt) ani.SetBool("技能3 - 移動 - Bool", true);

            else ani.SetBool("移動 - Bool", true);

            transform.localScale = scale;
        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, gameManager.playerxLimit.x, gameManager.playerxLimit.y), transform.position.y, 0);
        faceRight = _faceRight;
    }

    /// <summary>
    /// 技能
    /// </summary>
    /// <param name="_name">技能名稱</param>
    /// <param name="_time">技能CD</param>
    /// <param name="_cost">技能消耗</param>
    protected override void Skill(string _name, float _time, int _cost)
    {
        if (isUlt || isSkilling) return;
        if (rage < _cost) return;

        isSkilling = true;
        ani.SetTrigger(_name);
        skillTimer = _time;
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">傷害值</param>
    protected override void Hurt(int damage)
    {
        if (isUlt) return;

        // 防禦
        if (gameManager.holding_Defense && armor >= 0) armor -= damage;

        // 破防 or 未防禦
        else
        {
            if (hp > 0) ani.SetTrigger("受傷 - Trigger");

            hp -= damage;
        }

        hurtTimer = 0;

        // 死亡
        if (hp <= 0) Die();
    }

    /// <summary>
    /// 等待
    /// </summary>
    protected override void Idle()
    {
        moveDir = Vector3.zero;

        ani.SetBool("移動 - Bool", false);

        ani.SetBool("技能3 - 移動 - Bool", false);
    }

    /// <summary>
    /// 攻擊
    /// </summary>
    protected override void Attack()
    {
        if (isSkilling) return;

        if (isUlt && attackTimer > 1.417f)
        {
            ani.SetTrigger("技能3 - 攻擊 - Trigger");
            attackTimer = 0;
        }

        if (attackCount == 2 && attackTimer > playerdata.attackTime[2])
        {
            ani.SetTrigger("攻擊3 - Trigger");
            attackCount = 0;
            attackTimer = 0;
        }
        else if (attackCount == 1 && attackTimer > playerdata.attackTime[1])
        {
            ani.SetTrigger("攻擊2 - Trigger");
            attackCount += 1;
            attackTimer = 0;
        }
        else if (attackCount == 0 && attackTimer > playerdata.attackTime[0])
        {
            ani.SetTrigger("攻擊1 - Trigger");
            attackCount += 1;
            attackTimer = 0;
        }
    }

    /// <summary>
    /// 防禦
    /// </summary>
    protected override void Defense()
    {
        if (isSkilling || isUlt) return;

        ani.SetBool("防禦 - Bool", gameManager.holding_Defense);

        if (hurtTimer >= 6f && armor <= playerdata.armor) armor += 5;
    }

    /// <summary>
    /// 使用大招
    /// </summary>
    void Ultimate(int _cost)
    {
        if (rage < _cost) return;

        isUlt = true;

        ultTimer = 10f;
    }
}
