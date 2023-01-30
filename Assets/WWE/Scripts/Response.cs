using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace WWE
{

    public enum Emotion
    {
        None,
        Happy,
        Sad,
        Angry,

    }

    public class Response : MonoBehaviour
    {

        public Emotion emote;

        public Text reply;

        public int effect = 0;

        public int index = 0;

        public Text text;

        public Color defaultColor;

        private Button button;
        void Awake()
        {
            text = GetComponent<Text>();
            defaultColor = text.color;
            Reset();
        }


        void Reset()
        {

            if (button == null)
           button = GetComponent<Button>();

            button.enabled = true;

            button.interactable = false;
            button.interactable = true;
            button.OnDeselect(null);
        }
        void OnEnable()
        {
          
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Show(string text, float delay)
        {
                GetComponent<Text>().text =text;
            StartCoroutine(RunText(delay*0.1f));
            
        }

        IEnumerator RunText( float delay )
        {

            transform.localScale = Vector3.zero;
            yield return  new WaitForSeconds(delay);
            AudioController.Play(AudioController.Instance.selectText, 1, Random.Range(0.95f, 1.05f));
            
            float timer = 0;
            while (timer < 1)
            {
                timer += Time.deltaTime * 2;

                timer = Mathf.Clamp(timer, 0, 1);
                float scale = Mathf.Sin(timer * 2 * Mathf.PI) * 0.3f;
                scale *= (1 - timer);
                scale += 1;
                transform.localScale = scale * Vector3.one;

                yield return null;
            }
        }

        public void MouseClick()
        {
            if (text.text != "")
            {

                GetComponentInParent<WrestlerInfo>().SelectResponse(this);
                StartCoroutine(Flash());
                //  AudioController.Play(AudioController.Instance.selectText, 1, Random.Range(0.95f, 1.05f));
                AudioController.Play(AudioController.Instance.popUpTones[index], 1, Random.Range(0.95f, 1.05f));
            }
        }
        
        public void OnMouseOver()
        {
            if(text.text != "")
              AudioController.Play(AudioController.Instance.popUpTones[index], 1, Random.Range(0.95f, 1.05f));
        }

        IEnumerator Flash()
        {
            GetComponent<Button>().enabled = false;
            for (int i = 0; i < 3; i++)
            {
                yield return new WaitForSeconds(0.08f);
                text.color = Color.black;

                yield return new WaitForSeconds(0.08f);
                text.color = defaultColor;

            }
            text.text = "";
            GetComponent<Button>().enabled = true;

        }




    }
}