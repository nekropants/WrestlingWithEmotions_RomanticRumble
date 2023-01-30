using UnityEngine;
using System.Collections;
using System.Threading;

namespace Dance { 
 public class CameraMovement : MonoBehaviour
{
    public float freqX = 1;
    public float freqY = 1;
    public float freqR = 1;

    public float ampX = 1;
    public float ampY = 1;
    public float ampR = 1;

    public float tracking = 0;

    private Vector3 offset;
    private Quaternion offsetRot;

    private Camera cam;
    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.Inverse(offsetRot);
        transform.localPosition -= offset;

        offset.x = Mathf.Sin(Time.time * freqX) * ampX;
        offset.y = Mathf.Sin(Time.time * freqY) * ampY;
        offsetRot = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * freqR) * ampR);

        transform.rotation *= offsetRot;
        transform.localPosition += offset;


        cam.orthographicSize += tracking*Time.deltaTime;
    }
}

}