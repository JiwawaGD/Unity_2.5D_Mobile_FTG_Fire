using UnityEngine;

public class scr_Background : MonoBehaviour
{
    [Header("是否有速度變化")] public bool movable;
    [SerializeField] [Header("角色攝影機")] GameObject camera;

    void Awake()
    {
        camera = GameObject.Find("Camera");

    }

    void LateUpdate()
    {
        //Track();
    }

    void Track()
    {
        //if (movable)
        //{

        //}
        //else if (!movable)
        //{
        //    Vector3 t_camera = camera.transform.localPosition;

        //    t_camera.y = 1.8f;
        //    t_camera.z = 5f;

        //    transform.localPosition = t_camera;
        //}
    }

    public void Follow()
    {
        Vector3 temp;
        //= transform.localPosition;

        if (movable)
        {

        }
        else if (!movable)
        {
            temp = new Vector3(1, 0, 0);
        }


    }

    //void Follow()
    //{
    //    //temp = transform.localPosition;
    //    //followpos = followObject.transform.localPosition;

    //    ////followpos.x += 12f;
    //    //followpos.y = 2.35f;
    //    //followpos.z = 5f;

    //    //temp = Vector3.Lerp(temp, followpos, speed * Time.deltaTime);

    //    //transform.localPosition = temp;
    //}
}
