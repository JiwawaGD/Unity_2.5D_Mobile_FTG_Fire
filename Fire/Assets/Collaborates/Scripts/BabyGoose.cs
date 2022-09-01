using UnityEngine;

public class BabyGoose : MonoBehaviour
{
    [HideInInspector] public bool faceRight;

    readonly float speed = 9f;
    readonly float time = 3f;
    float destoryTimer;

    int jumpTriggerIndex;

    bool hasHitEnemy;

    Animator ani;

    void Awake()
    {
        ani = GetComponent<Animator>();
    }

    void Start()
    {
        jumpTriggerIndex = Animator.StringToHash("¸õ - Trigger");
    }

    void Update()
    {
        destoryTimer += Time.deltaTime;

        if (!hasHitEnemy)
        {
            transform.Translate(new Vector3(faceRight ? speed : -speed, 0, 0) * Time.deltaTime);

            if (destoryTimer >= 5) Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 10)
        {
            hasHitEnemy = true;
            ani.SetTrigger(jumpTriggerIndex);
            Destroy(gameObject, time);
        }
    }
}
