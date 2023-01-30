using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

namespace Dance { 
 public class Phrase : MonoBehaviour
{


    public Text text;

    float timer = 0;
    float hueScroll = 0;
    public AudioSource audioSource;
    public AudioClip badSound;
    public AudioClip goodSound;

    public bool isBad = false;


    public int rotDir = 1;

    void Awake()
    {
        text = GetComponent<Text>();


    }

    // Use this for initialization
    void Start()
    {
        audioSource.volume = 0.5f;
        audioSource.pitch = Random.Range(0.9f, 1.1f) - 0.4f;

        if (isBad)
        {
            audioSource.clip = badSound;
            StartCoroutine(PitchDown());

        }
        else
        {
            audioSource.clip = goodSound;
            audioSource.volume = 0.3f;
        }

        audioSource.Play();

        float h = 0;
        float s = 0;
        float v = 0;

        Color.RGBToHSV(text.color, out h, out s, out v);
        h = Random.value;

        Color col = Color.HSVToRGB(h, s, v);

        text.color = col;

    }

    public ColorCorrectionCurves CorrectionCurves;


    IEnumerator PitchDown()
    {
        print("PitchDown- " + CorrectionCurves.saturation);
        float range = 0.06f;
        float timer = 0;
        CorrectionCurves.saturation = 0;

        float speed = Random.Range(0.9f, 1.1f);

        print("PitchDown- " + CorrectionCurves.saturation);
        while (timer < 1)
        {
            timer += Time.deltaTime*speed;
            timer = Mathf.Clamp01(timer);
            AudioController.instance.danceTrack1.pitch = 1 - (timer)*range;


            yield return null;



        }

        print("PitchDown- " + CorrectionCurves.saturation);

        yield return new WaitForSeconds(0.1f);

        timer = 1;
        while (timer > 0)
        {
            timer -= Time.deltaTime*4*speed;
            timer = Mathf.Clamp01(timer);
            AudioController.instance.danceTrack1.pitch = 1 - (timer)*range;
            CorrectionCurves.saturation = 1 - timer;


            yield return null;
        }

        print("--- " +CorrectionCurves.saturation);

        yield return null;
    }


    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime/2f;
        timer = Mathf.Clamp01(timer);

        Color col = Color.white;

        if (isBad == false)
        {

            float h = 0;
            float s = 0;
            float v = 0;
            Color.RGBToHSV(text.color, out h, out s, out v);
            h += Time.deltaTime;

            col = Color.HSVToRGB(h, s, v);
        }

        col.a = (3 - timer*3);
        if (timer >= 1)
        {
            col.a = 0;
        }
            text.color = col;

        transform.localScale += Vector3.one*Time.deltaTime/4f;
        transform.Rotate(0, 0, Time.deltaTime*rotDir*4);

        if (timer >= 5)
                Destroy(gameObject);
    }
}

}