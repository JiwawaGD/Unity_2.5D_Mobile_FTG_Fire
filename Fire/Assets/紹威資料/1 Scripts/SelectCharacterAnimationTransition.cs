using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterAnimationTransition : MonoBehaviour
{
    [Header("動畫控制器")] Animator ani;
    Animation anim;

    int ChihuahuaSkill, ChihuahuaSkill2, ChihuahuaSkill3;
    int GooseSkill, GooseSkill2, GooseSkill3;

    void Start()
    {
        ani = GetComponent<Animator>(); 
        anim = GetComponent<Animation>();

        ChihuahuaSkill = Animator.StringToHash("招式一");
        ChihuahuaSkill2 = Animator.StringToHash("招式二");
        ChihuahuaSkill3 = Animator.StringToHash("招式三");
        GooseSkill = Animator.StringToHash("招式一");
        GooseSkill2 = Animator.StringToHash("招式二");
        GooseSkill3 = Animator.StringToHash("招式三");

    }
    public void Chihuahua11()
    {
        ani.SetTrigger(ChihuahuaSkill);
    }
    public void Chihuahua12()
    {
        ani.SetTrigger(ChihuahuaSkill2);
    }
    public void Chihuahua13()
    {
        ani.SetTrigger(ChihuahuaSkill3);
    }
    public void Goose()
    {
        ani.SetTrigger(GooseSkill);
    }
    public void Goose2()
    {
        ani.SetTrigger(GooseSkill2);
    }
    public void Goose3()
    {
        ani.SetTrigger(GooseSkill3);
    }
}
