using UnityEngine;
using System.Collections;

namespace Dance { 
 public class PanCamera : MonoBehaviour
{


    public float distance = 2;
    public float speed = 0;

    Vector3 offset;
    float timer = 0;

    bool started = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            started = true;
        }
        if (started == false)
        {
            return;
        }

        transform.localPosition -= offset;
        if (timer < distance)
        {
            timer += Time.deltaTime * speed;
            offset = Vector3.up * timer;

        }

        transform.localPosition += offset;
    }
}

}