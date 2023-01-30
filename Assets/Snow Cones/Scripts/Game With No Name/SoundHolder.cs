using UnityEngine;
using System.Collections;

public class SoundHolder : MonoBehaviour
{
    public AudioClip[] clips;

    public float timerInterval = 0.3f;
    public float randomIntervalOffset = 0;
    public float pitchVariance = 0.1f;

    float nextPlayTime = 0;
    public float volume = 1;

    int index = 0;


    public void TryPlay()
    {


        if (nextPlayTime < Time.time)
        {
            nextPlayTime = Time.time + timerInterval + Random.Range(-randomIntervalOffset, randomIntervalOffset);

            if (clips.Length > 1)
            {
                index = index + Random.Range(0, clips.Length - 1);
                index %= clips.Length;
            }
           SoundsController.audioSource.pitch = 1 + Random.Range(-pitchVariance, pitchVariance);


            SoundsController.audioSource.PlayOneShot(clips[index], volume);
        }
    }
}
