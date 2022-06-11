using UnityEngine;

public class test_Portal : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.tag == "ª±®a") scr_GameManager.NextLevel();
    }
}
