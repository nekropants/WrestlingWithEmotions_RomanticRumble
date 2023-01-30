using UnityEngine;
using System.Collections;

namespace Dance { 
 public class TalkToAnotherCharacter : MonoBehaviour
{
    public  Transform talkTo;
    private Eyes eyes;
    private Mouth mouth;

    private float timer = 0;
    // Use this for initialization
    void Start ()
    {
        eyes = GetComponentInChildren<Eyes>();
        mouth = GetComponentInChildren<Mouth>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    eyes.target = talkTo;
	    timer -= Time.deltaTime;
	    if (timer < 0)
	    {
            mouth.Speak(Random.Range(0.5f, 1.5f));
	        timer = Random.Range(1.5f, 3);
	    }
	}
}

}