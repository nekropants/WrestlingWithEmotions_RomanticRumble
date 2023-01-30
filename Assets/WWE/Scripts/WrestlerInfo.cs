using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace WWE
{
    public class WrestlerInfo : MonoBehaviour
    {
        public string hisText
        {
            set
            {
                _hisText.text = value;
                StartCoroutine(RunText());
            }

            get { return _hisText.text; }
        }

        public int absoluteIndex
        {
            get { return _absoluteIndex; }
        }

        public string name;
        public Characters character;
        public Text _hisText;
        public Text myText;
        public Response[] myOptions;

        private Vector3 opponentTextPosition;
        private Response selectedResponse;
        private Head head;

        public Image happyFace;
        public Image angryFace;
        public Image mouthClosedFace;
        public Image mouthOpenFace;


        public Animation animation;
        private DateInteraction interaction;

        public AudioClip song;
        public Sprite formAnswers;

        public int index;
       [SerializeField] private int _absoluteIndex; // used for looking up portraits

        public void SelectResponse(Response response)
        {
            selectedResponse = response;
        }

        void Awake()
        {
            head = GetComponentInChildren<Head>();
        }

        // Use this for initialization
        void Start()
        {
            for (int i = 0; i < 3; i++)
            {
                myOptions[i].index = i;
            }

            DeactivateAllText();

            StartCoroutine(RunConversation());
        }


        IEnumerator RunText()
        {
            float timer = 0;
            while (timer < 1)
            {
                timer += Time.deltaTime*2;

                timer = Mathf.Clamp(timer, 0, 1);
                float scale = Mathf.Sin(timer*2*Mathf.PI)*0.3f;
                scale *= (1 - timer);
                scale += 1;
                _hisText.transform.localScale = scale*Vector3.one;
                yield return null;
            }
        }


        private void DeactivateAllText()
        {
            _hisText.gameObject.SetActive(false);
            myText.gameObject.SetActive(false);

            for (int i = 0; i < 3; i++)
            {
                myOptions[i].gameObject.SetActive(false);
            }

        }

        public void SetInfo(DateInteraction dateInfo, int _index)
        {
            interaction = dateInfo;
            name = dateInfo.name;
            index = _index;
        }


        private Image nextFace;

        public Coroutine SetFace(Image face, float delay = 0)
        {
            return StartCoroutine(SetFaceRoutine(face, delay));
        }

        public IEnumerator SetFaceRoutine(Image face, float delay = 0)
        {

            nextFace = face;


            yield return new WaitForSeconds(delay);

            mouthClosedFace.gameObject.SetActive(false);
            mouthOpenFace.gameObject.SetActive(false);

            happyFace.gameObject.SetActive(false);
            angryFace.gameObject.SetActive(false);


            face.gameObject.SetActive(true);
        }

        public Coroutine Speak()
        {
            return StartCoroutine(SpeakRoutine());
        }

        public IEnumerator SpeakRoutine()
        {
            AudioController.Play(AudioController.Instance.popUp, 1, Random.Range(0.95f, 1.05f));

            cancelSpeak = false;
            yield return SetFace(mouthOpenFace);


            for (int i = 0; i < (int) (hisText.Split().Length*0.8f); i++)
            {
                if (cancelSpeak) yield break;

                yield return SetFace(mouthClosedFace, 0.08f);
                if (cancelSpeak) yield break;

                yield return SetFace(mouthOpenFace, 0.08f);
            }

            if (cancelSpeak) yield break;

            yield return new WaitForSeconds(0.15f);
            yield return SetFace(mouthClosedFace);
            if (cancelSpeak) yield break;

            yield return new WaitForSeconds(0.4f);

        }

        public int opinion = 0;


        Coroutine WaitForClick()
        {
            return StartCoroutine(WaitForClickRoutine());
        }

        public static IEnumerator WaitForClickRoutine()
        {
            while (Input.GetKeyDown(KeyCode.Mouse0) == false)
            {
                yield return null;
            }
            yield return null;

        }


        public GameObject myStickers;
        public static GameObject stickerTransform;
        public float delay = 0.3f;

        IEnumerator RunConversation()
        {

            SetFace(mouthClosedFace);
            yield return new WaitForSeconds(2.15f);
            SceneController.instance.PlaySitDown();
            ZoomCam.instance.StopIntroAudio();

            Gaze.instance.lookDown = false;

            yield return new WaitForSeconds(0.12f);
            Table.Shake();

            yield return new WaitForSeconds(1.5f);

            // --- introduction ---
            _hisText.gameObject.SetActive(true);
            print(gameObject);
            hisText = interaction.intro;
            yield return Speak();


            RandomizeResponses();

            print(" ---- 1");
            Table.instance.nameOnForm.text = interaction.name;
            SceneController.instance.form.Clear();
            SceneController.instance.formDescriptors.Refresh();


            if (stickerTransform)
                stickerTransform.gameObject.SetActive(false);

            stickerTransform = new GameObject("stickers " + interaction.name);
            stickerTransform.transform.parent = SceneController.instance.form.transform;
            stickerTransform.transform.localPosition = Vector3.zero;
            stickerTransform.transform.localScale = Vector3.one;

            myStickers = stickerTransform;

            Gaze.instance.formCompleted = false;

            // --- Salutation ---
            print(" ---- 2");



            myText.gameObject.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                myOptions[i].gameObject.SetActive(true);
                myOptions[i].Show(interaction.salutations[i], i);
            }

            while (selectedResponse == null)
            {
                yield return null;
            }


            // --- show my answer ---
            int answerIndex = selectedResponse.index;

            for (int i = 0; i < 3; i++)
            {
                if (myOptions[i] != selectedResponse)
                    myOptions[i].gameObject.SetActive(false);
            }

            switch (interaction.salutationPoints[answerIndex])
            {
                case 0:
                    SetFace(angryFace);
                    break;

                case 1:
                    SetFace(happyFace);
                    break;

                case 2:
                    SetFace(happyFace);
                    break;
            }

            print(" ---- 3");

            hisText = "";

            opinion += interaction.salutationPoints[answerIndex];

            print(" ---- 4");

            yield return new WaitForSeconds(1f);
            SetFace(mouthClosedFace);
            yield return new WaitForSeconds(0.4f);

            // --- First Question ---
            myText.gameObject.SetActive(false);
            hisText = interaction.dateQuestion;
            myOptions[answerIndex].gameObject.SetActive(false);

            yield return Speak();


            yield return new WaitForSeconds(0.2f);
            head.tiltHead = true;

            print(" ---- 5");


            // --- My Answer ---
            selectedResponse = null;
            myText.gameObject.SetActive(false);
            for (int i = 0; i < 3; i++)
            {
                myOptions[i].gameObject.SetActive(true);
                myOptions[i].Show(interaction.myAnswers[i], i);
            }

            while (selectedResponse == null)
            {
                yield return null;
            }

            print(" ---- 6");

            // --- show my answer ---
            answerIndex = selectedResponse.index;

            for (int i = 0; i < 3; i++)
            {
                if (myOptions[i] != selectedResponse)
                    myOptions[i].gameObject.SetActive(false);
            }


            head.tiltHead = false;
            yield return new WaitForSeconds(0.2f);

            // --- show response ---

            hisText = interaction.dateResponses[answerIndex];

            switch (interaction.replyPoints[answerIndex])
            {
                case 0:
                    head.shake = true;
                    SetFace(angryFace);
                    break;
                    
                case 1:
                    head.nod = true;
                    SetFace(happyFace);
                    break;

                case 2:
                    head.nod = true;
                    SetFace(happyFace);
                    break;
            }

            opinion += interaction.replyPoints[answerIndex];
            print("points " + interaction.replyPoints[answerIndex]);

            print(" ---- 7");


        

            print(" ---- 8");
            SetFace(mouthClosedFace, 2);

            yield return StartCoroutine(WaitForClickRoutine());


            print(" ---- 9");

            hisText = "";
            myText.text = "";

            print(" ---- 10");



            yield return new WaitForSeconds(0.3f);


            // --- show my questions ---
            selectedResponse = null;
            for (int i = 0; i < 3; i++)
            {
                myOptions[i].gameObject.SetActive(true);
                myOptions[i].Show(interaction.myQuestions[i], i);
            }


            // --- show date response ---

            while (selectedResponse == null)
            {
                yield return null;
            }

            print(" ---- 11");


            // --- show my answer ---
            answerIndex = selectedResponse.index;

            for (int i = 0; i < 3; i++)
            {
                if (myOptions[i] != selectedResponse)
                    myOptions[i].gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.2f);

            hisText = interaction.dateResponses2[answerIndex];
            yield return Speak();


            // --- buzzer ---
            formAnswers = Table.instance.form.sprite;
            yield return new WaitForSeconds(1.5f);


            print(" ---- 14");;
            print(" ---- 15");

            SceneController.instance.HitBuzzer();
            yield return new WaitForSeconds(0.5f);


            yield return StartCoroutine(WaitForClickRoutine());

            hisText = "";


            print(" ---- 16");
            yield return new WaitForSeconds(0.8f);

            // --- indicate opinion ---
            print("Final opinion " + opinion);

            bool responded = false;
            string[] nameTokens = DressWrestler.chosenName.Split();
            if (opinion < 2)
            {
                foreach (var kp in interaction.negativeResponses)
                {
                    if (kp.Key != "" && DressWrestler.chosenName.ToLower().Contains(kp.Key.ToLower()))
                    {
                        responded = true;
                        hisText = kp.Value;
                        print(kp.Key + "  " + kp.Value);
                        break;
                    }
                }
            }
            else if (opinion > 2)
            {
                foreach (var kp in interaction.positiveResponses)
                {
                    print(kp.Key + "  " + kp.Value);

                    if (kp.Value != "" && DressWrestler.chosenName.ToLower().Contains(kp.Key.ToLower()))
                    {
                        responded = true;
                        hisText = kp.Value;
                        print(kp.Key + " - " + kp.Value);
                        break;

                    }
                }
            }

            if (responded == false)
            {
                hisText = interaction.nuetralResponse;
                print("nuetral  " + interaction.nuetralResponse);
            }



            yield return Speak();


            yield return new WaitForSeconds(0.5f);



            // --- show my questions ---
            selectedResponse = null;
            for (int i = 0; i < 3; i++)
            {
                myOptions[i].gameObject.SetActive(true);
                myOptions[i].Show(interaction.goodByes[i], i);
            }


            // --- show date response ---

            while (selectedResponse == null)
            {
                yield return null;
            }

            answerIndex = selectedResponse.index;
            print(" interaction.goodByePoints[answerIndex]  " + interaction.goodByePoints[answerIndex]);

            int feeling = interaction.goodByePoints[answerIndex];
            if (feeling < 0)
            {
                SetFace(angryFace);

            }
            else if (feeling == 0)
            {
                SetFace(mouthClosedFace);

            }
            else
            {
                SetFace(happyFace);

            }



            print(" ---- 20");

            opinion += interaction.goodByePoints[answerIndex];
            // --- show my answer ---

            for (int i = 0; i < 3; i++)
            {
                if (myOptions[i] != selectedResponse)
                    myOptions[i].gameObject.SetActive(false);
            }
            DeactivateAllText();


            yield return new WaitForSeconds(0.5f);


            // --- date leaves ---
            // SetFace(mouthClosedFace);


            GetComponentInChildren<Animator>().Play("Arms Leave Anim");
            AudioController.Play(AudioController.Instance.getUpSound, 0.3f);
            Table.Shake();
            yield return new WaitForSeconds(1.5f);

            Gaze.instance.lookDown = true;

            while (Gaze.instance.formCompleted == false)
            {
                yield return null;

            }
            yield return new WaitForSeconds(0.5f);

            SceneController.instance.ShowNextScene();

            yield return null;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F6))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKey(KeyCode.Mouse0))
                cancelSpeak = true;

        }

        bool cancelSpeak = false;

        void OnGUI()
        {
            if (Application.isEditor == false)
                return;
            GUI.color = Color.black;
        }


        void RandomizeResponses()
        {
            for (int i = 0; i < 8; i++)
            {
                int a = Random.Range(0, 1);
                int b = Random.Range(0, 1);

                Vector3 temp = myOptions[a].transform.position;
                myOptions[a].transform.position = myOptions[b].transform.position;
                myOptions[b].transform.position = temp;
            }
        }

    }



}