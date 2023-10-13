using UnityEngine;
using System.Collections;

namespace WWE
{
    public class Gaze : MonoBehaviour
    {
        public bool formCompleted = false;
        public bool lookDown = false;
        public float downDist = 100;
        private Vector3 defaultPos;

        public static Gaze instance;

        public float offset = 0;
        // Use this for initialization
        void Awake()
        {
            defaultPos = transform.localPosition;

            instance = this;
        }

        // Update is called once per frame
        void Update()
        {

            Vector3 target = defaultPos;
            // if (Input.GetKeyDown(KeyCode.Space) && Application.isEditor)
            //     lookDown = !lookDown;

            if (lookDown)
            {
                target -= Vector3.down*downDist;

            }


            transform.localPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime*3);
            offset = (transform.localPosition - Vector3.down*downDist).magnitude;
        }
    }
}