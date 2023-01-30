using System;
using UnityEngine;
using System.Collections;

public class FerrisWheelHandController : MonoBase
{
    public Transform shoulder;


    public float yLimit = -1000;


    public bool PullAway = false;
    public bool holdHand = false;
    public Vector3 handPos;

    public GameObject smileFace;
    public GameObject lookAwayface;


    public Transform shockedMouth;
    public Transform smileMouth;

    private float smileTimer = 0;
    private float happyfaceTimer = 3;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{

	    bool smile = (smileTimer <= 0);

	    if (smileTimer > 0)
	        smileTimer -= Time.deltaTime;

            smileMouth.gameObject.SetActive(smile);
            shockedMouth.gameObject.SetActive(!smile);


	    if (holdHand)
	    {
	        Vector3 target = shoulder.transform.position + Vector3.left*10;
	        transform.transform.position = Vector3.Lerp(transform.transform.position, handPos, Time.fixedDeltaTime*6);

	        happyfaceTimer -= Time.deltaTime;
	        if (happyfaceTimer < 0)
	        {

	            lookAwayface.gameObject.SetActive(true);
	            smileFace.gameObject.SetActive(false);
	        }
	    }
	    else if (PullAway)
	    {
	        Vector3 target = shoulder.transform.position + Vector3.left*10;
	        transform.transform.position = Vector3.Lerp(transform.transform.position, target, Time.fixedDeltaTime*6);

	        smileTimer = 0.8f;
	        if ((target - transform.transform.position).magnitude < 0.1f)
	        {
	            PullAway = false;
	        }
	    }
	    else
	    {

	        float speed = 40;

	        Vector3 direction = Vector3.zero;

	        if (Up)
	            direction += Vector3.up;

	        if (Left)
	            direction += Vector3.left;

	        if (Down)
	            direction += Vector3.down;

	        if (Right)
	            direction += Vector3.right;

	        transform.transform.position += direction*speed*Time.fixedDeltaTime;
	    }

	    float maxDist = 50;
        Vector3 diff = transform.position - shoulder.transform.position;

	    if (diff.magnitude > maxDist)
	    {
            transform.position = shoulder.transform.position + diff.normalized * maxDist;
	    }



	    Vector3 pos = transform.position;

        pos.y = Mathf.Max(pos.y, yLimit);
        if (pos.y < yLimit) ;
        {
           // pos.y = yLimit;
        }

	    transform.position = pos;

	}
}
