using UnityEngine;
using System.Collections;

public class PlaySound : MonoBehaviour {

	// Use this for initialization

    public Shake shake;
	void Start () {
	
	}



    void OnDisable()
    {
        
        print("On DIsable");
    }
    public void OpenDoorSound()
    {
        WWE.AudioController.Play(WWE.AudioController.Instance.trailerDoor, 1, 0.8f);
        shake.AddShake(true);
    }

    public void PullHandle()
    {
        WWE.AudioController.Play(WWE.AudioController.Instance.trailerHandle, 0.5f, Random.Range(0.9f, 1.1f));
        shake.AddShake(.2f);
    }

 
}
