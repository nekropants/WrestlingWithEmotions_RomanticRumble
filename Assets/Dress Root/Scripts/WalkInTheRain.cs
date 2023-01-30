using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Dance { 
 public class WalkInTheRain : MonoBehaviour
{

    public SpeechBubble thoughBubble;

    public GameObject narrationBubble;
    public Text narrationText;

    public GameObject Titles;
    public AudioSource mainTrack;


public SpriteRenderer fader;
    void Awake()
    {
        Titles.gameObject.SetActive(false);

                Player.hasBeer = false;

                fader.enabled = true;
    }

    private Vector3 defaultNarrationPos;
    private Vector3 offsetPos;

    void ShowNarration()
    {
        narrationBubble.transform.localPosition = defaultNarrationPos;
    }
    void HideNarration()
    {
        narrationBubble.transform.localPosition = offsetPos;
    }
    // Use this for initialization
    IEnumerator Start () {


        AudioController.instance.walkTrack.Play();
        AudioController.instance.rainTrack.Play();

        defaultNarrationPos = narrationBubble.transform.localPosition;
        offsetPos = defaultNarrationPos;
        offsetPos.y -= 20000;

        HideNarration();

        float walktrackvolume=AudioController.instance.walkTrack.volume ;
		float raintrackvolume=AudioController.instance.rainTrack.volume ;
        AudioController.instance.walkTrack.volume = 0;
        AudioController.instance.rainTrack.volume = 0;
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime/4f;
            t = Mathf.Clamp01(t);

            AudioController.instance.walkTrack.volume = t*walktrackvolume;
            AudioController.instance.rainTrack.volume = t*raintrackvolume;

           fader.color = new Color(0,0,0, 0.5f + ( 1 -t)*2);
            

            yield return null;
        }
                fader.enabled = false;



        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;

        ShowNarration();

        narrationText.text = ("I remember thinking ");
        narrationBubble.gameObject.SetActive(true);
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        thoughBubble.Show("Why Am I doing this?");
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        thoughBubble.Hide();
        

        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        ShowNarration();

        narrationText.text = "Walking through that rain, worried my makeup would start running any minute, I just kept thinking the words:";
        yield return null;


        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        thoughBubble.Show("Dress To Express Dancing Success" );
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        HideNarration();

        thoughBubble.Hide();
        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        ShowNarration();
        narrationText.text = "\"Dress To Express Dancing Success\" - the coolest club in town.";
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
       // narrationBubble.gameObject.SetActive(true);
        narrationText.text = "I'm no dancer. No, I was going there for the dressing up.";
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
       // narrationBubble.gameObject.SetActive(true);
        narrationText.text = "Too check out the wildest fashionistas in town, and show off my hottest threads.";
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
      //  narrationBubble.gameObject.SetActive(true);
        narrationText.text = "To see and be seen.";
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        //narrationBubble.gameObject.SetActive(true);
        narrationText.text = "...";
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
       // narrationBubble.gameObject.SetActive(true);
        narrationText.text = "And maybe... to find the love of my life.";
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        HideNarration();

        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;

        mainTrack.enabled = false;
        Titles.gameObject.SetActive(true);


    }

    // Update is called once per frame
    void Update () {
	
	}
}

}