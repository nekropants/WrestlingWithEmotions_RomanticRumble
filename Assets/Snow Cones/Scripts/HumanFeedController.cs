using UnityEngine;
using System.Collections;

public class HumanFeedController : MonoBehaviour
{

    public AudioClip handImpact1;
    public AudioClip handImpact2;
    public AudioClip humanAppear;

    public Shake shake;
    public void ShakeScreen()
    {
        shake.AddShake(3, 1, 1);
    }



    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void PlayHandImpactSound1()
    {
        AudioController.Play(handImpact1, 0.5f);
    }

    private void PlayHandImpactSound2()
    {
        AudioController.Play(handImpact1, 0.5f);
    }

    private void PlayHumanAppearSound()
    {
        AudioController.Play(humanAppear, 0.5f);
    }

    public AudioClip humanSounds;

    public void PlayHumanSounds()
    {
        AudioController.Play(humanSounds, 0.5f);

    }
}
