using UnityEngine;
using System.Collections;

namespace WWE
{


    public class Parallax : MonoBehaviour
    {
        public float m = 0;
         float _m = 0.0001f;
        public bool flipX = false;
        // Use this for initialization


        private Vector3 offset = Vector3.zero;

        private float limit = 50000;

        private Vector3 mousePrev;
        void Start()
        {
            mousePrev = Input.mousePosition;
        }

        // Update is called once per frame
        void Update()
        {

            transform.localPosition -= offset;
            Vector3 delta = (Input.mousePosition - mousePrev)*m*_m;
            if(flipX)
                delta.x = -delta.x;

            offset -= delta;
            mousePrev = Input.mousePosition;

            float lim = limit*m*_m;
                
            offset.x = Mathf.Clamp(offset.x, -lim, lim);
            offset.y = Mathf.Clamp(offset.y, -lim, lim);

            transform.localPosition += offset;

        }
    }
}