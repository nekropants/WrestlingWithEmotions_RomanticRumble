using UnityEngine;
using System.Collections;

namespace WWE
{


    public class ThoughtBubble : MonoBehaviour
    {
        private float timer;
        public TextMesh textMesh;

        public float bounce = 0.1f;
        public bool showing = false;
        // Use this for initialization

        public static ThoughtBubble instance;

        void Start()
        {
            instance = this;
            textMesh = GetComponentInChildren<TextMesh>();
            textMesh.text = "";
            transform.localScale = Vector3.zero;
        }

        public void Show(string str)
        {
            showing = true;
            textMesh.text = str;
            StartCoroutine(BounceText());
            AudioController.Play(AudioController.Instance.popUp);
        }

        string str;

        public void Hide()
        {
            if (showing)
            {
                showing = false;
                StopAllCoroutines();
                StartCoroutine(HideRoutine());
            }
        }
        IEnumerator HideRoutine()
        {
           

            transform.localScale = Vector3.zero;
            float timer = 0;
            while (timer < 1)
            {
                timer += Time.deltaTime*10;

                float scale = (1 - timer);
              //  scale += 1;
                transform.localScale = scale*Vector3.one;

                yield return null;
            }
                transform.localScale =  Vector3.zero;
        }
        // Update is called once per frame
        void Update()
        {

        }

        IEnumerator BounceText()
        {

            transform.localScale = Vector3.zero;
            float timer = 0;
            while (timer < 1)
            {
                timer += Time.deltaTime*2;

                timer = Mathf.Clamp(timer, 0, 1);
                float scale = Mathf.Sin(timer*2*Mathf.PI)*0.3f;
                scale *= (1 - timer);
                scale += 1;
                transform.localScale = scale*Vector3.one;


                yield return null;
            }
        }
    }
}