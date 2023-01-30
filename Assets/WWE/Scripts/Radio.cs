using UnityEngine;
using System.Collections;
using WWE;
using AudioController = WWE.AudioController;

public class Radio : MonoBehaviour
{
    private int direction = 1;

    private Animator anim;

    public Transform[] turnyThings;

    public float speed = 360;
    private bool paused =false;

    // Use this for initialization
    void Start ()
	{
	    anim = GetComponentInChildren<Animator>(true);

	}


    private bool waitForClick = false;

	// Update is called once per frame
	void Update () {
	    if (waitForClick && Input.GetKeyDown(KeyCode.Mouse0)) 
	    {
	       // waitForClick = false;
	       // anim.speed = 1;
	    }


	    if (paused == false)
	    {
	        foreach (Transform turnyThing in turnyThings)
	        {
	            turnyThing.Rotate(0, 0, -Time.deltaTime*speed*direction);
	        }
	    }
	}

    public void Stop()
    {
        paused = true;
        AudioSequence.Stop();
        AudioSequence.StartAmbience();

        WWE.AudioController.Play(WWE.AudioController.Instance.radioStop, 1, Random.Range(0.9f, 1.1f));
        //anim.speed = 0;
        // waitForClick = true;
    }
    AudioSource rewinding;
    public void ChangeDirection()
    {
        direction = -direction;
	    WWE.AudioController.Play(WWE.AudioController.Instance.radioRewind, 1, Random.Range(0.9f, 1.1f));
        rewinding = WWE.AudioController.Play(WWE.AudioController.Instance.radioRewinding, 1, Random.Range(0.9f, 1.1f));
        // anim.speed = 0;
        //  waitForClick = true;
    }

    public void Play()
    {
        paused = false;

        WWE.AudioController.Play(WWE.AudioController.Instance.radioStop, 1, Random.Range(0.9f, 1.1f));
        AudioSequence.StopAmbience();
        ExitTrailer.instance.PlayMainTheme();
        if(rewinding)
            rewinding.Stop();
    }

    public void ChangeScene()
    {
        DressWrestler.instance.ChangeSceneRoutine();
    }
}
