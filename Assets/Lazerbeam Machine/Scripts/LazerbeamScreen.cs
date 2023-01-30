using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Dance;

public class LazerbeamScreen : MonoBehaviour
{

    public Transform left;
    public Transform right;

    public ParticleGenerator particles;

    private bool leftHighlighted = false;

    public SpriteRenderer WhiteOut;

    public AudioClip[] bubbleSounds;

    private float bubbleSoundTimer;
    public float bubbleSoundInterval = 0.2f;

    public AudioSource soundtrack;
    public bool playBubbleSounds = true;

    // Use this for initialization
    void Start ()
    {
        WaitForClick.OVERRIDE = true;
    }

    // Update is called once per frame
    void Update()
    {
        bubbleSoundTimer += Time.deltaTime;

        if (playBubbleSounds)
        {
            while (bubbleSoundTimer > bubbleSoundInterval)
            {
                bubbleSoundTimer -= bubbleSoundInterval;
                bubbleSoundTimer -= Random.value * bubbleSoundInterval;
                AudioController.Play(bubbleSounds.RandomElement(), Random.Range(0.5f, 1) * 0.5f, Random.Range(0.3f, 0.7f));
            }
        }

       // if (DemoVid.instance.open)
       // {
       //     soundtrack.volume = Mathf.Lerp(soundtrack.volume, 0, Time.time * 0.01f);
       ////     print(soundtrack + " " + soundtrack.volume);
       // }
       // else
       // {
       //     soundtrack.volume = Mathf.Lerp(soundtrack.volume, 1, Time.time * 0.5f);
       // }
    }
    //public void SelectLazerJam()
    //{
    //   DemoVid.instance.Open();
    //}
    //public void SelectDance()
    //{
    //   StartCoroutine(  Transition("WalkToClub"));
    //}
    //public void SelectSnowCones()
    //{
    //   StartCoroutine(  Transition("Snowcone Romantics"));
    //}

    //public void SelectWWE()
    //{
    //   StartCoroutine(  Transition("Trailer"));
    //}

    IEnumerator Transition(string scene)
    {
        particles.gameObject.SetActive(true);
        bubbleSoundInterval /= 5;
        float timer = 0;

        while (timer < 1)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp01(timer);

            WhiteOut.color = Color.Lerp(new Color(0, 0, 0, 0), Color.white, timer);
        yield return null;
        }

        yield return new WaitForSeconds(1);
        yield return null;
       // UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);

    }
}
