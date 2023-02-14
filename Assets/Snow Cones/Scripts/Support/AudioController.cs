using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    private static List<AudioSource> sources = new List<AudioSource>();
    public static AudioController Instance;


    private void Awake()
    {
        Instance = this;
    }

    private static void Init()
    {
        if (Instance == null)
        {
            GameObject go = new GameObject("AudioController");
            Instance = go.AddComponent<AudioController>();
            // DontDestroyOnLoad(go);
        }
    }

    public static AudioSource GetAudioSource()
    {
        for (int i = sources.Count - 1; i >= 0; i--)
        {
            if (sources[i] == null)
            {
                sources.RemoveAt(i);
            }
            else if (sources[i].isPlaying == false)
            {
                return sources[i];
            }
        }

        GameObject go = new GameObject("");
        AudioSource source = go.AddComponent<AudioSource>();
        sources.Add(source);
        source.playOnAwake = false;
        go.transform.parent = Instance.transform;
        go.transform.localPosition = Vector3.zero;
        return source;
    }



    public static AudioSource Play(AudioClip clip)
    {
        return Play(clip, 1, 1);
    }
    public static AudioSource Play(AudioClip clip, float volume)
    {
        return Play(clip, volume, 1);
    }

    public static AudioSource Play(AudioClip clip, float volume, float pitch)
    {
        Init();

        if(clip == null)
            return null;

        AudioSource source = GetAudioSource();
        source.name = "" + clip.name;
        source.clip = clip;
        source.Play();
        source.pitch = pitch;
        source.volume = volume;
        return source;

    }
}
