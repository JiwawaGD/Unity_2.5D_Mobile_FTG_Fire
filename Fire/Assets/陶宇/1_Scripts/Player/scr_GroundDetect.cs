using UnityEngine;

public class scr_GroundDetect : MonoBehaviour
{
    scr_PlayerController playerController;

    void Awake()
    {
        playerController = GetComponentInParent<scr_PlayerController>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == this.gameObject) return;

        playerController.isGrounded = true;
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject == this.gameObject) return;

        playerController.isGrounded = true;
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject == this.gameObject) return;

        playerController.isGrounded = false;
    }
}
