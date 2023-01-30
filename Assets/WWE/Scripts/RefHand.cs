using UnityEngine;
using System.Collections;


namespace WWE
{
	

public class RefHand : MonoBehaviour {

public AudioClip[] hitSounds;
int index =0;

public 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    
    public void HitGround()
    {
		
        AudioController.Play(hitSounds[index]);
		 AudioController.Play(WWE.AudioController.Instance.tapout, 1, Random.Range(0.95f, 1.05f));

        index ++;
    }
}
}