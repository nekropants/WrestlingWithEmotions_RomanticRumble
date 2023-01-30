using UnityEngine;
using System.Collections;

public class LakeRoll : MonoBase
{

    private SpriteSM sprite;

	// Use this for initialization
	void Start ()
	{
	    sprite = GetComponent<SpriteSM>();
	}
    float angle = 0;

    float turnSpeed = 0;
    // Update is called once per frame
	void Update ()
	{

        bool RollLeft = Left;
        bool RollRight = Right;

        bool TurnLeft = Up;
        bool TurnRight = Down;

        if (RollLeft)
	        angle = -180;

        if (RollRight)
            angle =  180;



	    RageSpline spline;


        //transform.RotateAround(transform.position - transform.up * 160, Vector3.forward, angle *Time.deltaTime);


        turnSpeed = Mathf.Lerp(turnSpeed, 0, Time.deltaTime * 3);
        if (RightUp)
            turnSpeed = -5;

        if (LeftUp)
            turnSpeed = 5;

        transform.RotateAround(transform.position , Vector3.forward, turnSpeed);


        Vector3 velocity = Quaternion.AngleAxis(Mathf.Sign(angle ), Vector3.forward)*transform.right * angle*3;

        GetComponent<Rigidbody>().velocity = velocity;
        GetComponent<Rigidbody>().angularVelocity = -Vector3.forward * (-turnSpeed + angle*0.01f);

        angle = Mathf.Lerp(angle, 0, Time.deltaTime * 3);

	    Animate();
	}

    private int frame = 0;
    private float timer = 0;
    private float timerInterval = 0.1f;
    private void Animate()
    {
        timer += Time.deltaTime;

        while (timer > timerInterval)
        {
            timer -= timerInterval;

            if (Left)
                frame--;

            if(Right)
                frame++;

            frame += 5;
            frame %= 5;
            sprite.SetLowerLeftPixel_X(( frame)*sprite.width);
        }


    }
}
