using UnityEngine;
using System.Collections;
using SplineEditor;
using UnityEngine.SceneManagement;

namespace Dance { 
 public class ThanksForPlaying : MonoBehaviour
{
    public SplineFollow[] splines;


    public Transform JayHead;
    public Transform BenHead;
    public Transform RichHead;

    public GameObject logo;
    public static ThanksForPlaying instance;
    public Camera vhs1;
    public Camera vhs2;
    void Awake()
    {
        instance = this;
    }

    public SpeechBubble richSpeech;
    public SpeechBubble benSpeech;
    public SpeechBubble jaySpeech;
    // Use this for initialization
    IEnumerator Start ()
    {

        richSpeech.target = RichHead;
        benSpeech.target = BenHead;
        jaySpeech.target = JayHead;

        richSpeech.followRotation = false;
        benSpeech.followRotation = false;
        jaySpeech.followRotation = false;
        yield return null;

        yield return  new WaitForSeconds(2f);


        foreach (SplineFollow spline in splines)
        {
        yield return  new WaitForSeconds(1f);
            spline.enter = true;
        }

        yield return  new WaitForSeconds(5f);

        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        yield return null;

        benSpeech.Show("Thanks For Playing");
        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;


        //rich.eyes.target = null;
        //Jay.eyes.target = rich.eyes.transform;
        //ben.eyes.target = rich.eyes.transform; ;

        benSpeech.Hide();

        if (DanceEvaluator.gaveUp)
        {
            richSpeech.Show("Turns out you suck at dancing...");

            yield return null;
            while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;

            richSpeech.Hide();
            benSpeech.Show("You know what they say...");

            yield return null;
            while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;

            benSpeech.Hide();
            jaySpeech.Show("Better to give up and fall over...");

            yield return null;
            while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;

            jaySpeech.Hide();
            richSpeech.Show("...than have anyone think you were actually trying");

        }
        else
        {
            richSpeech.Show("Clutch move list!");
        }
        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;

        //rich.eyes.target = ben.eyes.transform;
        //Jay.eyes.target = ben.eyes.transform;
        //ben.eyes.target = null;
        richSpeech.Hide();


        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;

        jaySpeech.Show("Shoot us a tweet @teamlazerbeam");

        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        jaySpeech.Hide();


        richSpeech.Show("We'd love to hear your thoughts!");
        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        richSpeech.Hide();


        benSpeech.Show("Bye!");
        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        benSpeech.Hide();

        foreach (SplineFollow spline in splines)
        {
            spline.exit = true;
         yield return  new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(2f);

        //rich.eyes.target = null;
        //Jay.eyes.target = null;
        //ben.eyes.target = null;

        yield return StartCoroutine(LerpInOutroStatic());

        yield return null;
        while (Input.GetKeyDown(KeyCode.Mouse0) == false) yield return null;
        SceneManager.LoadScene(0);
        Destroy(AudioController.instance.gameObject);


    }

    // Update is called once per frame
    void Update () {
        if (Application.isEditor && Input.GetKeyDown(KeyCode.Tab))
        {
            vhs1.gameObject.SetActive(vhs1.gameObject.activeSelf == false);
            vhs2.gameObject.SetActive(vhs1.gameObject.activeSelf == false);
        }

	}

    void OnDestroy()
    {
       
    }


    IEnumerator LerpInOutroStatic()
    {
        float timer = 0;
        //vhs1.gameObject.SetActive(false);
        //vhs2.gameObject.SetActive(vhs1.gameObject.activeSelf == false);
        AudioController.instance.tvStatic.Play();
        AudioController.instance.tvStatic.volume = 0;
        yield return null;
        while (timer < 1)
        {
            timer += Time.deltaTime/3f;
            timer = Mathf.Clamp01(timer);
            Vector3 pos = Vector3.zero;
            pos.x = Random.value*0.8f*timer;
            pos.z = 0;
            logo.transform.localPosition = pos;
            AudioController.instance.tvStatic.volume = timer;

            yield return null;
        }
        AudioController.instance.tvStatic.Stop();
        AudioController.instance.glitchStatic.Stop();

        //vhs1.gameObject.SetActive(true);
        //vhs2.gameObject.SetActive(vhs1.gameObject.activeSelf == false);
        logo.gameObject.SetActive(false);
        Cloud.ReplaceAll();


    }
}

}