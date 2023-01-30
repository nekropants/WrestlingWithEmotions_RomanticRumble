using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace WWE
{
    public class ZoomCam : MonoBehaviour
    {
        public AnimationCurve curve;

        private float offset = 0;
        public float frequency = 1;
        public float amplitude = 1;

        public Camera currentCam;


        public static ZoomCam instance;


        // Use this for initialization
        void Start()
        {
            instance = this;

        }

        public void StopIntroAudio()
        {

        }

        // Update is called once per frame
        void Update()
        {

            transform.localScale -= Vector3.one*offset;

            offset = curve.Evaluate(Time.time*frequency)*amplitude;

            transform.localScale += Vector3.one*offset;

            

        }


        public void TeleportLogo()
        {
            Vector3 vp = currentCam.WorldToViewportPoint(transform.position);

            transform.position = Camera.main.ViewportToWorldPoint(vp);
            transform.localScale *=  Camera.main.orthographicSize / currentCam.orthographicSize ;
            print(Camera.main.ViewportToWorldPoint(vp )+ " " + vp);
            transform.parent = CamController.instance.transform;
                }
    }
}