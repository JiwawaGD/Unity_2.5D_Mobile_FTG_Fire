using UnityEngine;

public class scr_GroundDetect : MonoBehaviour
{
    scr_PlayerBase playerBase;

    void Awake()
    {
        playerBase = GetComponentInParent<scr_PlayerBase>();
    }

    void Start()
    {
        IgnoreLayer();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == this.gameObject) return;

        playerBase.isGrounded = true;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject == this.gameObject) return;

        playerBase.isGrounded = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == this.gameObject) return;

        playerBase.isGrounded = false;
    }

    void IgnoreLayer()
    {
        Physics.IgnoreLayerCollision(9, 10);
    }
}
