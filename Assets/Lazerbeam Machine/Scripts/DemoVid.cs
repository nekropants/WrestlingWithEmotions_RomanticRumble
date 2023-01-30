using UnityEngine;
using System.Collections;
//using System.Security.Policy;

public class DemoVid : MonoBehaviour
{

    public MeshRenderer video;

    public GameObject question;

    public static DemoVid instance;
    public bool open = true;

    public AnimationCurve curve;
        Vector3 defaultScale = Vector3.one;

    public void Open()
    {
        //transform.localScale = Vector3.zero;
        //open = true;
        //gameObject.SetActive(true);

        //if (video)
        //{
        //    MovieTexture movie = video.material.mainTexture as MovieTexture;
        //    movie.loop = false;
        //    movie.Stop();
        //    movie.Play();

        //    //
        //    AudioSource audio = gameObject.GetComponent<AudioSource>();
        //    if(!audio)
        //        audio=  gameObject.AddComponent<AudioSource>();

        //    audio.clip = movie.audioClip;
        //    audio.loop = false;
        //    audio.Stop();
        //    audio.Play();
        //}
        //question.SetActive(false);

    }

    public void Close()
    {
        gameObject.SetActive(false);
        open = false;
        question.SetActive(true);
    }
    // Use this for initialization
    void Start ()
    {
        defaultScale = transform.localScale;
        instance = this;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
        Vector3 target = Vector3.zero;
	    
	    if(open)
            target = defaultScale;

	    transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime*20);

    }

    void OnMouseDown()
    {
        gameObject.SetActive(false);
    }
}
