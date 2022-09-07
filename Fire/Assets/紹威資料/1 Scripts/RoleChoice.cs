using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoleChoice : MonoBehaviour
{
    public GameObject[] figure;
    //public GameObject canScroll;

    public GameObject[] GewawaSkillUI1;
    //public GameObject GewawaSkillUI2;
    //public GameObject GewawaSkillUI3;
    public GameObject[] GooseSkillUI;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(canScroll.transform.position.x);
    }
    public void ScrollCalueChange()
    {
        //ChangeGewawa();
        //ChandeGoose();
    }

    public void ChandeGoose()
    {
        figure[1].gameObject.SetActive(true);
        GewawaSkillUI1[0].gameObject.SetActive(true);
        GewawaSkillUI1[1].gameObject.SetActive(true);
        GewawaSkillUI1[2].gameObject.SetActive(true);
        figure[0].gameObject.SetActive(false);
        GooseSkillUI[0].gameObject.SetActive(false);
        GooseSkillUI[1].gameObject.SetActive(false);
        GooseSkillUI[2].gameObject.SetActive(false);
    }

    public void ChangeGewawa()
    {
        figure[0].gameObject.SetActive(true);
        GooseSkillUI[0].gameObject.SetActive(true);
        GooseSkillUI[1].gameObject.SetActive(true);
        GooseSkillUI[2].gameObject.SetActive(true);
        figure[1].gameObject.SetActive(false);
        GewawaSkillUI1[0].gameObject.SetActive(false);
        GewawaSkillUI1[1].gameObject.SetActive(false);
        GewawaSkillUI1[2].gameObject.SetActive(false);
    }
}
