using UnityEngine;

public class test_Portal : MonoBehaviour
{
    [SerializeField] [Header("關卡場景")] int sceneBuildIndex;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "玩家") scr_GameManager.ChooseLevel(sceneBuildIndex);
    }
}
