using UnityEngine;
using System.Collections;

public class AudioSequence : MonoBehaviour {


    public static AudioSequence instance;
    public AudioClip intro;
    public AudioClip loop;
    public AudioSource source;
    public AudioSource ambience;
    float defaultVol;
    // Use this for initialization

    void Awake()
    {
        instance = this;
    }
    IEnumerator Start () {


        defaultVol = source.volume;
        //source.volume = defaultVol/3f;x

        source.clip = intro;
        source.loop = false;
        source.Play();
        

        yield return new WaitForSeconds(intro.length);
        StopAmbience();
        TrailerCam.instance.Switch();
        source.Stop();
        source.clip = loop;
        source.loop = true;
        source.Play();

    }

 


    // Update is called once per frame
    void Update () {

    }
    public static void StopAmbience()
    {
        if (instance)
            instance.ambience.Stop();

    }

    public static void StartAmbience()
    {
        if (instance)
            instance.ambience.Play();

    }



    public static void Stop()
    {
        if(instance)

            instance.source.Stop();
    }

    public void TurnOffLowPass()
    {
        //GetComponent<AudioLowPassFilter>().enabled = false;
        //source.volume = defaultVol / 2f;
        //ambience.Stop();

     
    }

}
