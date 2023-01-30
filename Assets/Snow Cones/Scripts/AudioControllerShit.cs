using UnityEngine;
using System.Collections;

public class AudioControllerShit : Singleton<AudioControllerShit>
{

    public AudioSource oneShotAudioSource;
    public AudioSource soundtrackAudioSource;
    public static void Play(AudioClip clip)
    {
        Instance.oneShotAudioSource.PlayOneShot(clip);
    }


    public static void Play(AudioClip clip, float pitch, float volume)
    {
        Instance.oneShotAudioSource.PlayOneShot(clip, volume);
    }

    public static void PlaySong(AudioClip clip)
    {
        if(  Instance.soundtrackAudioSource.clip == clip)
            return;
        
        Instance.soundtrackAudioSource.clip = clip;
        Instance.soundtrackAudioSource.loop = true;
        Instance.soundtrackAudioSource.Play();
    }


    // Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKeyDown(KeyCode.F8))
        {
            soundtrackAudioSource.enabled = !soundtrackAudioSource.enabled;
        }
	}
}
