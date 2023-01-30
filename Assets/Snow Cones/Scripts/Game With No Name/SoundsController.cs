using UnityEngine;
using System.Collections;

public class SoundsController : Singleton<SoundsController> 
{
    public SoundHolder bathroomTileFootSteps;
    public SoundHolder scrubSounds;


    public static AudioSource audioSource
    {
        get
        {
            return Instance.GetComponent<AudioSource>();
        }

    }


}
