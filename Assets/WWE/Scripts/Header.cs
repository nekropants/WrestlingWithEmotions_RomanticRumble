using UnityEngine;
using System.Collections;

namespace WWE
{
    public class Header : MonoBehaviour
    {
        public Transform[] firstLayer;
        public Transform[] secondLayer;


        public float frequency = 1;
        public float amplitude = 1;

        public float offset = 1f;


        Vector3 offset1 = Vector3.zero;
        Vector3 offset2 = Vector3.zero;
        Vector3 offset3 = Vector3.zero;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            //transform.localPosition -= offset1;
            //   offset1 = Vector3.up*Mathf.Sin(Time.time* frequency) * amplitude;
            //   transform.localPosition += offset1;

            foreach (Transform transform1 in firstLayer)
            {
                transform1.localPosition -= offset2;
            }
            offset2 = Vector3.up*Mathf.Sin((Time.time*frequency) + offset)*amplitude;

            foreach (Transform transform1 in firstLayer)
            {
                transform1.localPosition += offset2;
            }

            foreach (Transform transform1 in secondLayer)
            {
                transform1.localPosition -= offset3;
            }

            offset3 = Vector3.up*Mathf.Sin(Time.time*frequency + offset*2)*amplitude;

            foreach (Transform transform1 in secondLayer)
            {
                transform1.localPosition += offset3;
            }
        }
    }
}