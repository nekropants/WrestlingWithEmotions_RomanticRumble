using UnityEngine;
using System.Collections;
//using System.Runtime.Remoting.Messaging;
using Battlehub.SplineEditor;

namespace SplineEditor
{
    public class SplineFollow : MonoBehaviour
    {
        public float Duration = 15.0f;
        public SplineBase Spline;
        public float Offset;
        private float m_t;

        public float stopAt = 0.4f;
        private Vector3 defaultScale;

        public  bool enter = false;
        public  bool exit = false;


        public AudioClip tone;
        public float maxVolume = 0.5f;
        private AudioSource audio; 
        private float Wrap(float t)
        {

            return Mathf.Clamp(t, 0, Duration);
            return (Duration + t % Duration) % Duration;
        }



        void Awake()
        {
            audio = gameObject.AddComponent<AudioSource>();
            audio.clip = tone;
            audio.loop = true;
            audio.Play();
            audio.volume = 0;

            StartCoroutine(PitchIn());
        }

        IEnumerator PitchIn()
        {
            while (enter == false)
            {
                yield return    null;
            }
            yield return new WaitForSeconds(2);

            while (audio.volume < maxVolume)
            {
                audio.volume += Time.deltaTime*0.2f;
                audio.volume = Mathf.Clamp(audio.volume, 0, maxVolume);
                yield return null;
            }

            yield return new WaitForSeconds(4);

            while (audio.volume >0 )
            {
                audio.volume -= Time.deltaTime * 0.2f;
                audio.volume = Mathf.Clamp(audio.volume, 0.02f, maxVolume);
                yield return null;
            }
        }

        private void Update()
        {
            if (enter)
                Move();
        }

        private void Move()
        {
            float t = Wrap(m_t + Offset * Duration / 50.0f);
            float v = Spline.GetVelocity(t / Duration).magnitude / 5.0f;

            float boost = 1;

            if (exit)
                boost = 3;

            if (exit == false &&  t / Duration > stopAt)
            {
                return;
            }

         
            if (m_t >= Duration)
            {
             //   m_t = (m_t - Duration) + Time.deltaTime*boost / v;

            }
            else
            {
                m_t += Time.deltaTime*boost / v;
            }


            float lerp = t/Duration;
            lerp *= 2;
            lerp = Mathf.Clamp01(lerp);


           
            transform.localScale = defaultScale - (Vector3.one*lerp *0.8f);
            Vector3 position = Spline.GetPoint(t / Duration);
            Vector3 dir = Spline.GetDirection(t / Duration);
            float twist = Spline.GetTwist(t / Duration);

            transform.position = position;
            transform.LookAt(position + dir, Vector3.forward);
            //transform.RotateAround(position, dir, twist);


        }

    }

}
