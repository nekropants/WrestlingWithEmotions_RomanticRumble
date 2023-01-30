using UnityEngine;
using System.Collections;

namespace Dance { 
[RequireComponent(typeof(AudioSource))]
 public class AudioSequence : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;
    private int index = 0;
    private float timer = 0;
	// Use this for initialization
	void Start ()
	{
	    audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    timer -= Time.deltaTime;
	    if (timer <= 0)
	    {
	        index += Random.Range(1, clips.Length - 1);
	        index %= clips.Length;

	        audioSource.clip = clips[index];
            audioSource.Play();


	        timer = audioSource.clip.length;

	    }
	}
}

}