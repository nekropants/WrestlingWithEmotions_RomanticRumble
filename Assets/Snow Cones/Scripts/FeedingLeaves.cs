using UnityEngine;
using System.Collections;

public class FeedingLeaves : MonoBehaviour
{

    private Shake shake;

    private SoundHolder soundHolder;

	// Use this for initialization
	void Start ()
	{
	    shake = GetComponent<Shake>();
	    lastRustle = Time.time;
	    soundHolder = GetComponent<SoundHolder>();
	}


    private float lastRustle = 0;
	// Update is called once per frame
	void Update () {
	    if (lastRustle + 10 < Time.time)
	    {
	        ResetRustleTime();
            shake.AddShake();
            soundHolder.TryPlay();
        }
	}

    void ResetRustleTime()
    {
        lastRustle = Time.time + Random.Range(0,2);
    }

    //void OnTriggerEnter(Collider other)
    //{

    //    print("OnTriggerEnter  "+ other);
    //}


    //void OnCollisionEnter(Collision collision)
    //{
    //    print("OnCollisionEnter  " + collision);
    
    //}
    private void OnTriggerExit2D(Collider2D other)
    {
        ResetRustleTime();
        shake.AddShake();
        soundHolder.TryPlay();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        ResetRustleTime();
        shake.AddShake();
        soundHolder.TryPlay();

        print("OnTriggerEnter2D  " );
    }
}
