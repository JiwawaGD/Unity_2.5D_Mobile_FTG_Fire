using UnityEngine;

public class playerTest : MonoBehaviour
{
    Rigidbody2D rigid2D;

    float walk = 30f;
    float maxWalk = 2.0f;

     void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        int move = 0;
        if (Input.GetKey(KeyCode.RightArrow)) move = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) move = -1;

        float speed = Mathf.Abs(this.rigid2D.velocity.x);

        if (speed<this.maxWalk)
        {
            this.rigid2D.AddForce(transform.right * move * this.walk);
        }
    }
}
