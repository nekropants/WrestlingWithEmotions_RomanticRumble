using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WWE
{
    public class Manny : MonoBehaviour
    {

        public AnimationCurve yCurve;
        public float amplitude = 1;
        public float frequency = 1;

        public float walkSpeed = 1;

        private float offscreenDistance = 1500;

        public static Manny instance;
        public Image openMouth;
        public Image closedMouth;

        public Sprite armsDown;
        public Sprite micUp;
        public Sprite armsUp;

        private Text text;
        // Use this for initialization
        void Awake()
        {
            instance = this;
        }

        private float speakTimer = 0;

        IEnumerator SpeakRoutine(int flaps)
        {
            
                        AudioController.Play(AudioController.Instance.popUp, 1, Random.Range(0.95f, 1.05f));

            for (int i = 0; i < flaps; i++)
            {


                openMouth.gameObject.SetActive(true);
                closedMouth.gameObject.SetActive(false);

                yield return new WaitForSeconds(0.15f);

                openMouth.gameObject.SetActive(false);
                closedMouth.gameObject.SetActive(true);

                yield return new WaitForSeconds(0.1f);
            }
        }


        public static void TriggerManny()
        {
            if(instance)
                instance.StartCoroutine(instance.Announce());
        }

        IEnumerator Announce()
        {



            text = GetComponentInChildren<Text>();
            text.text = "";

            yield return StartCoroutine(WalkOnStage());

            for (int i = 0; i < ScriptReader.mannysIntro.Length; i++)
            {
                text.text = ScriptReader.mannysIntro[i];

                StopCoroutine("SpeakRoutine");
                StartCoroutine(SpeakRoutine(text.text.Split(' ' ).Length/2));

                yield return StartCoroutine(WrestlerInfo.WaitForClickRoutine());
            }

            text.text = "";

            StartCoroutine(TriggerDates());
            StartCoroutine(WalkOffStage());
        }

        IEnumerator TriggerDates()
        {
            yield return new WaitForSeconds(1f);
            SceneController.BringInWrestler();
        }

   

        IEnumerator WalkOffStage()
        {
            GetComponent<Image>().sprite = armsUp;


            StopCoroutine("SpeakRoutine");
            openMouth.gameObject.SetActive(true);
            closedMouth.gameObject.SetActive(false);
            yCurve.postWrapMode = WrapMode.Loop;

            float timer = 0;

            float yOffset = 0;
            Vector3 offset = Vector3.zero;


            while (timer < 1)
            {
                timer += Time.deltaTime * walkSpeed;
                transform.localPosition -= offset;
                offset = offscreenDistance * timer * Vector3.left;
                transform.localPosition += offset;

                transform.position -= Vector3.up * yOffset;
                yOffset = yCurve.Evaluate(timer * frequency) * amplitude;
                transform.position += Vector3.up * yOffset;
                yield return null;
            }



            yield return null;
        }

        IEnumerator WalkOnStage()
        {

            GetComponent<Image>().sprite = micUp;


            transform.localPosition -= Vector3.right* offscreenDistance;
            yield return new WaitForSeconds(1.5f);

            transform.localPosition += Vector3.right * offscreenDistance;

            yCurve.postWrapMode = WrapMode.Loop;

            float timer = 1;

            float yOffset = 0;
            Vector3 offset = Vector3.zero;
            


            while (timer > 0)
            {
                timer -= Time.deltaTime*walkSpeed;
                transform.localPosition -= offset;

                offset = offscreenDistance*timer*Vector3.right;
                transform.localPosition += offset;

                transform.position -= Vector3.up * yOffset;
                yOffset = yCurve.Evaluate(timer * frequency) * amplitude;
                transform.position += Vector3.up * yOffset;
                yield return null;
            }
            GetComponent<Image>().sprite = armsDown;

            yield return null;
        }

        // Update is called once per frame
        void Update()
        {



        }
    }
}