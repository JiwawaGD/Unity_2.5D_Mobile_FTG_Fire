using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour
{
    float speed = 6000f;
    float time = 2f;

    Rigidbody rig;

    void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Fire();
    }

    void Fire()
    {
        rig.velocity = new Vector3(speed, 0, 0) * Time.deltaTime;

        Destroy(gameObject, time);
    }
}
