using UnityEngine;
using System.Collections;

public class HumanHands : MonoBase
{
    public float speed = 100;
    public float dist = 100;
    public SpriteRenderer hand;
    public Rigidbody2D handRBody;
    public Transform shoulder;

    public bool BeingPulledOn = false;

    public Sprite emptyHandSprite;
    public Sprite grassInHandSprite;

    public float resistenceMin = 200;
    public float resistenceMax = 300;
    public float freq = 1;
    public float freq2 = 1;

    private float armExtension = 0;

    public bool hasLeafs = false;


    public CircleCollider2D bucketTrigger;

	// Use this for initialization
	void Start ()
	{
	}

    private void GrabLeaves()
    {
        hand.sprite = grassInHandSprite;
        hand.enabled = true;
        hasLeafs = true;
    }

    public void FeedHuman()
    {
        hasLeafs = false;
        hand.enabled = false;
    }



    public void ReleaseHand()
    {
        hand.enabled = true;
        hand.sprite = emptyHandSprite;
        speedM = 3;
    }

    private Vector3 bodyOffset = Vector3.zero;
	
    float speedM = 1;

	// Update is called once per frame
	void Update ()
	{

	    Vector3 dir = Vector3.zero;
	    if (Left)
	        dir += Vector3.left;
        if (Right)
            dir += Vector3.right;
        if (Down)
            dir += Vector3.down;
        if (Up)
            dir += Vector3.up;

        Vector3 force = dir * Time.deltaTime * speed * speedM;
	    speedM = Mathf.Lerp(speedM, 1, Time.deltaTime*5);
     
	    if (BeingPulledOn)
	    {

            float resistence = resistenceMin + (resistenceMax - resistenceMin) * Extensions.NormalizedSin(Time.time * freq);
            resistence *= 0.6f + Extensions.NormalizedSin(Time.time * freq2);



            force += resistence * Vector3.right * Time.deltaTime;
	    }


	    handRBody.AddForce(force);
        Vector3 offset = hand.transform.position - shoulder.transform.position;
        if (offset.magnitude > dist)
        {
            offset = offset.normalized * dist;
        }
        else if (offset.magnitude <10 )
        {
            offset = offset.normalized * 10;
        }

        Vector3 pos = shoulder.transform.position + offset;
        hand.transform.position = pos;



        transform.position -= bodyOffset;

        bodyOffset.x = (hand.transform.localPosition.x - dist * 0.5f - 140) * 0.2f;
        transform.position += bodyOffset;


        if (hasLeafs == false && bucketTrigger.OverlapPoint(hand.transform.position)
     )
        {
            GrabLeaves();
        }


	}
}
