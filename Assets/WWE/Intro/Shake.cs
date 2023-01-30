using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

    namespace Dance { 
 public class Shake : MonoBehaviour
    {


        protected Vector3 offset;
        //
        public float frequencyX = 0;
        public float frequencyY = 0;
        protected float counterX = 0;
        protected float counterY = 0;
        public float amplitudeX = 0;
        public float amplitudeY = 0;

        public float damping = 2;
        public float freqDamping = 2;
        public bool applyDampimg = true;



        private float startFrequencyX = 0;
        private float startFrequencyY = 0;
        private float startAmplitudeX = 0;
        private float startAmplitudeY = 0;


        public bool shakeOnStart = false;

        public void AddShake(bool damping = true)
        {
            enabled = true;
            frequencyX = startFrequencyX;
            frequencyY = startFrequencyY;
            amplitudeX = startAmplitudeX;
            amplitudeY = startAmplitudeY;
        }

        //
        public void AddShake(float amplitude, float freqX, float freqY)
        {
            amplitudeX = amplitude;
            amplitudeY = amplitude;
            frequencyX = (32 + Random.value*34)*freqX;
            frequencyY = (30 + Random.value*34)*freqY;

            counterX = Mathf.PI*freqX;
            counterY = Mathf.PI*freqY;
        }


        public void AddShake2(float AmplitudeX, float AmplitudeY, float freq)
        {
            amplitudeX = Mathf.Max(amplitudeX, AmplitudeX);
            amplitudeY = Mathf.Max(amplitudeY, AmplitudeY);

            float newFreqX = Random.Range(0.9f, 1.1f)*freq;
            float newFreqY = Random.Range(0.9f, 1.1f)*freq;
            frequencyX = Mathf.Max(frequencyX, newFreqX);
            frequencyY = Mathf.Max(frequencyY, newFreqY);

            counterX = Mathf.PI;
            counterY = Mathf.PI;
        }

        void OnDisable()
        {
            transform.position -= offset;
            offset = Vector3.zero;
        }


        void Awake()
        {
            counterX = Random.value*Mathf.PI*2;
            counterY = Random.value*Mathf.PI*2;

            startFrequencyX = frequencyX;
            startFrequencyY = frequencyY;
            startAmplitudeX = amplitudeX;
            startAmplitudeY = amplitudeY;

            if (shakeOnStart == false)
            {
                frequencyX = 0;
                frequencyY = 0;
                amplitudeX = 0;
                amplitudeY = 0;
            }
        }

        // Update is called once per frame
        void LateUpdate()
        {
            float t = Time.deltaTime;

            counterX += frequencyX*t;
            counterY += frequencyY*t;

            if (applyDampimg)
            {
                if (frequencyX > 0)
                {
                    frequencyX -= t*damping;
                    if (frequencyX < 0.01f)
                        frequencyX = 0;

                }

                if (frequencyY > 0)
                {
                    frequencyY -= t*damping;
                    if (frequencyY < 0.01f)
                        frequencyY = 0;

                }
            }

            if (amplitudeX > 0 || amplitudeY > 0)
            {
                if (applyDampimg)
                {
                    amplitudeX -= t*damping;
                    if (amplitudeX <= 0)
                    {
                        amplitudeX = 0;
                    }

                    amplitudeY -= t*damping;
                    if (amplitudeY <= 0)
                    {
                        amplitudeY = 0;
                    }
                }

                transform.position -= offset;

                float x = Mathf.Sin(counterX)*amplitudeX;
                float y = -Mathf.Sin(counterY)*amplitudeY;
                offset = transform.up*y + transform.right*x;
                transform.position += offset;
            }

        }
    }

}