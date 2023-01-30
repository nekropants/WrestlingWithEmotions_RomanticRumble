using System;
using UnityEngine;
using System.Collections;

namespace WWE
{
    public class Head : MonoBehaviour
    {
        public bool tiltHead = false;
        public bool nod = false;
        public bool shake = false;

        public float amplitude = 10;
        public int shakes = 1;

        public float blend = 0;

        public float nodeTimer = 0;

        private Vector3 offset;

        public AnimationCurve blendCurve;

        // Use this for initialization

        private float rot = 0;
        // Update is called once per frame
        void Update()
        {
            
            float rotTarget = 0;
            if (tiltHead)
            {
                rotTarget = 20;
            }

            rot = Mathf.Lerp(rot, rotTarget, Time.deltaTime*10);
            transform.rotation = Quaternion.Euler(0, 0, rot);

            transform.localPosition -= offset;

            if (nod || shake)
            {
                nodeTimer += Time.deltaTime*4;
                if (nodeTimer > shakes)
                {
                    nod = false;
                    shake = false;
                    nodeTimer = shakes;
                }

                if (nod)
                    offset.y = Mathf.Sin(nodeTimer*Mathf.PI)*amplitude;
                if (shake)
                    offset.x = Mathf.Sin(nodeTimer*Mathf.PI)*amplitude;
                //       blend = Mathf.Lerp(blend, 1, Time.deltaTime * blendRate);
            }
            else
            {
                // blend = Mathf.Lerp(blend, 0, Time.deltaTime* blendRate);
                offset.x = 0;
            }
            blend = blendCurve.Evaluate((nodeTimer/shakes));


            offset *= blend;

            transform.localPosition += offset;

        }
    }
}
