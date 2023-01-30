using UnityEngine;
using System.Collections;

namespace Dance { 
 public class TrackCamera : MonoBehaviour
{


    public float distance = 2;
    public float speed = 0.04f;

    float offset;
    float timer = 0;
    public Camera cam1;
    public Camera cam2;
    public bool started = false;
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

        cam1.orthographicSize -= offset;
        if (timer < distance)
        {
            timer += Time.deltaTime * speed;
            offset = timer;

        }

        cam1.orthographicSize += offset;
        cam2.orthographicSize = cam1.orthographicSize;
    }
}

}