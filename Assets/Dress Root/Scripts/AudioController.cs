using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

namespace Dance { 
 public class AudioController : MonoBehaviour {


public static AudioController instance;
	// Use this for initialization

	public AudioSource rainTrack;
	public AudioSource walkTrack;
	public AudioSource outsideClub;
	public AudioSource insideClub;
	public AudioSource danceTrack1;
	public AudioSource danceTrack2;
	public AudioSource bubbleSound;
    public AudioSource crowd;
    public AudioSource neon;
	public AudioSource drumroll;
	public AudioSource OutroTrack;
	public AudioSource glitchStatic;
	public AudioSource tvStatic;
    public AudioMixerGroup OutroTrackFilteredMixer;

    void Awake () {
	
		
		if(instance != null)
		{
			Destroy(gameObject);
			return;
		}

	

		DontDestroyOnLoad(gameObject);
		instance = this;

		
	}


    void OnLeveWasLoaded()
    {
        {
            print(insideClub.isPlaying);
            //if (insideClub.isPlaying == false)
            //    insideClub.Play();
       
        }
    }
    void Start()
	{
		OnLeveWasLoaded ();
		//PlayOutroTrack ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
  


	public void FadeInBarMusic()
	{
		StartCoroutine(FadeInBarMusicRoutine());
	}

	

	public void PlayMuffledDanceTrack()
	{
		insideClub.Stop();
        print("STOP");
		danceTrack1.Play();
		//danceTrack1muffled.Play();
		//danceTrack1.volume = 0;
	}

    public void ChangeSong()
    {
        danceTrack1.Stop();
        danceTrack2.Play();
        //danceTrack1muffled.Play();
        //danceTrack1.volume = 0;
    }

    //public IEnumerator ChangeSongRoutine()
    //{
    //    while (danceTrack1.volume > 0.1f)
    //    {
            
    //    }
    //}

    void OnDestroy()
    {

    }

    IEnumerator FadeInBarMusicRoutine()
	{
		insideClub.Play();
		outsideClub.Play();
		insideClub.volume = 0;

		float timer = 0;

		while (timer < 1)
		{

			timer += Time.deltaTime*0.3f;
			timer = Mathf.Clamp01(timer);

			outsideClub.volume = timer;			
			walkTrack.volume = 1 - timer;
            yield return null;
		}

		yield return null;

	}



	IEnumerator PlayOutroMusicRoutine()
	{
		danceTrack1.Stop ();
		danceTrack2.Stop ();
		drumroll.Play ();
		Debug.Log ("stop " + OutroTrack.loop);
		float startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup - startTime < drumroll.clip.length) 
		{
			yield return null;
		}
		drumroll.Stop ();
		OutroTrack.Play ();

		Debug.Log ("OutroTrack " + OutroTrack.loop);

	}

	public void PlayOutroTrack()
	{
		StartCoroutine (PlayOutroMusicRoutine ());
	}
}

}