using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderSettingAnim : MonoBehaviour
{
    public GameObject Setting;
    public void ShowSetting()
    {
        if (Setting != null)
        {
            Animator animator = Setting.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("show");
                animator.SetBool("show", !isOpen);
            }
        }
    }

}
