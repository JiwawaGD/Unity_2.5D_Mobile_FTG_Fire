using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterAnimationTransition : MonoBehaviour
{
    [Header("�ʵe���")] Animator ani;
    Animation anim;

    int ChihuahuaSkill, ChihuahuaSkill2, ChihuahuaSkill3;
    int GooseSkill, GooseSkill2, GooseSkill3;

    void Start()
    {
        ani = GetComponent<Animator>(); 
        anim = GetComponent<Animation>();

        ChihuahuaSkill = Animator.StringToHash("�ۦ��@");
        ChihuahuaSkill2 = Animator.StringToHash("�ۦ��G");
        ChihuahuaSkill3 = Animator.StringToHash("�ۦ��T");
        GooseSkill = Animator.StringToHash("�ۦ��@");
        GooseSkill2 = Animator.StringToHash("�ۦ��G");
        GooseSkill3 = Animator.StringToHash("�ۦ��T");

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
