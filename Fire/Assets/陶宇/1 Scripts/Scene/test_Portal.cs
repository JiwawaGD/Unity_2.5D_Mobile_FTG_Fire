using UnityEngine;

public class test_Portal : MonoBehaviour
{
    [SerializeField] [Header("���d����")] int sceneBuildIndex;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "���a") scr_GameManager.ChooseLevel(sceneBuildIndex);
    }
}
