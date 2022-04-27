using UnityEngine;

public class scr_GroundDetect_1 : MonoBehaviour
{
    scr_PlayerController_1 playerController;

    void Awake()
    {
        playerController = GetComponentInParent<scr_PlayerController_1>();
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
