using UnityEngine;
using System.Collections;

namespace WWE
{

    public class Arm : MonoBehaviour
    {
        Camera cam;
        public Transform pencilBase;
        public Transform pencilTip;
        public Rigidbody2D root;

        public float range = 100;

        public static Vector3 clampedMousePos;

        private float offset = 0;
        // Use this for initialization
        void Start()
        {
            cam = GetComponentInChildren<Camera>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {

            cam.transform.position -= offset*Vector3.up;
            offset = -Gaze.instance.offset;
            cam.transform.position += offset*Vector3.up;

            //if (Input.GetKey(KeyCode.Mouse0))
            {


                Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                pencilBase.transform.position = Vector3.Lerp(pencilBase.transform.position, pos, Time.deltaTime*10);
                pencilBase.transform.position = pos;


                Vector3 dist = pencilBase.transform.position - root.transform.position;


                if (dist.magnitude >= range)
                {
                    dist = dist.normalized*range;
                    pencilBase.transform.position = root.transform.position + dist;

                }

                clampedMousePos = cam.WorldToScreenPoint(pencilTip.transform.position);
            }
        }
    }
}
