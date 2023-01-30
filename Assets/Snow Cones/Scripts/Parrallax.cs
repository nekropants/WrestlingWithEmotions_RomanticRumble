using System.Timers;
using UnityEngine;
using System.Collections;

public class Parrallax : MonoBehaviour
{


    public float speed = 100;
    private float lerp = 0;

    public bool paused = true;

    // Update is called once per frame
	void Update ()
	{
        if(paused )
            return;
	    lerp += Time.deltaTime;
	    lerp = Mathf.Clamp01(lerp);


        transform.position -= Time.deltaTime * speed * Vector3.down * lerp;
	}
}
