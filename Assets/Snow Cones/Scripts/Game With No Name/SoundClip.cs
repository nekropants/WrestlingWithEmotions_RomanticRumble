using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SoundClip : MonoBehaviour
{

    public AudioClip clip;

    private AudioSource source;
    public bool fadeIn = false;
    public float fadeSpeed = 1;
    public float volume = 1;

    void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        source.loop = true;
        source.clip = clip;
        if (fadeIn)
            source.volume = 0;
    }

	// Use this for initialization
	void Start ()
	{

	    source.Play();
	
	}
	
	// Update is called once per frame
	void Update () {
        if (fadeIn)
        {
            source.volume += Time.deltaTime * fadeSpeed*volume;
        }
	    source.volume = Mathf.Clamp(source.volume, 0, volume);
	}
}
