using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleChoice : MonoBehaviour
{
    public GameObject[] figure;
    public GameObject canScroll;
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
        if (330 < canScroll.transform.position.x) figure[0].gameObject.SetActive(true);
        else figure[0].gameObject.SetActive(false);
        if (330 > canScroll.transform.position.x) figure[1].gameObject.SetActive(true);
        else figure[1].gameObject.SetActive(false);
    }
}
