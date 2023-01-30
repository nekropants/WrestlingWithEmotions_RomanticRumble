using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections;

public class WalkOnDinos : MonoBase
{
    private SpriteSM sprite;

    public float maxSpeed = 100;
    public float maxFallspeed = 20;
    public float jumpStrength = 20;


    private float yi = 0;
    private float xi = 0;
    public float gravity = 10;
    public float moveAccel = 10;
    public float drag = 0.9f;


    public float followDistance = 0;


    public BoxCollider2D endTrigger;
    public BoxCollider2D jumpTrigger;
    public BoxCollider2D snowTrigger;

    public TriggerCondition leftInputCondition;
    public TriggerCondition rightInputCondition;
    public TriggerCondition upInputCondition;

    private enum State
    {
        Idle,
        Walking,
        Jumping,
        Sliding,
        Talking,
    }

    private State state = State.Idle;


    private int direction = 1;
	// Use this for initialization
	void Start ()
	{
	    sprite = GetComponentInChildren<SpriteSM>();
	    talkGesture = Random.Range(0, 2);

	    if (AI == false)
	        following = transform;
	}


    private static Transform following;
    public bool AI = false;
    public int aiOffset = 100; 

    private int talkGesture = 0;


    private bool hasJumpedOff  = false;
    private bool atEnd = false;
    private void CheckEndTrigger()
    {


        if (endTrigger.bounds.Contains(transform.position))
            atEnd = true;


        if (jumpTrigger.bounds.Contains(transform.position) && AI && hasJumpedOff == false)
        {
            xi = 0;
            hasJumpedOff = true;
            Jump();
        }
    }

    private static int conesInSnow = 0;
    private void CheckSnowTrigger()
    {




        if (snowTrigger.bounds.Contains(transform.position) )
        {
            gameObject.SetActive(false);
            conesInSnow++;
            if (conesInSnow > 1)
            {
                SceneController.ChangeScene(SceneEnum.SnowAngels);
            }
        }
    }

    private void ChangeScene()
    {

        print("ChangeScene");
        SceneController.ChangeScene(SceneEnum.SnowAngels);
    }


    private bool OnSlope()
    {
        return angle > 15 && angle < 90;
    }

    // Update is called once per frame
	void FixedUpdate ()
	{
	    CheckEndTrigger();
        CheckSnowTrigger();

        bool leftDown = false;
        bool upDown = false;
        bool right = false;


        if (AI == false)
        {
             leftDown = LeftDown;
             upDown = UpDown;
             right = Right;


            if (upInputCondition && upInputCondition.Evaluate())
            {
                upDown = true;
            }
            if (leftInputCondition && leftInputCondition.Evaluate())
            {
                leftDown = true;
            }
            if(rightInputCondition && rightInputCondition.Evaluate())
            {
                right = true;
            }
            //if (state == State.Sliding)
            //    right = true;

            leftInput.Add(leftDown);
            rightInput.Add(right);
            upInput.Add(upDown);
        }
        else
        {

            if (leftInput.Count > aiOffset)
            {
                leftDown = leftInput[0];
                leftInput.RemoveAt(0);
            }

            if (rightInput.Count > aiOffset)
            {


                right = rightInput[0];
                rightInput.RemoveAt(0);

                if (Vector3.Distance(following.position, transform.position) < followDistance)
                {
                    right = false;
                }
                else if (Vector3.Distance(following.position, transform.position) > followDistance*4)
                {
                    right = true;
                }
            }

            if (upInput.Count > aiOffset)
            {
                upDown = upInput[0];
                upInput.RemoveAt(0);
            }

            if (atEnd && state != State.Jumping)
            {
                right = true;
                upDown = false;
                leftDown = false;
            }
        }

	    if (state != State.Jumping)
	    {

	        if (state == State.Sliding && !OnSlope())
	        {
	            if (xi < 4f || (xi < 5f && right))
	            {
	                state = State.Idle;
	            }

	        }
	        else if (xi > 1 && OnSlope() && atEnd == false) // dont slide at end
	        {
	            //   yi = 2;
	            state = State.Sliding;
	        }
	        else if (leftDown)
	        {
	            if (AI == false)
	                direction = -1;
	            state = State.Talking;
	            talkGesture++;
	            talkGesture %= 3;
	            frame = 0;
	            frameCounter = 0;
	        }
            else if (upDown)
            {
                Jump();
            }
	        else if (right)
	        {
	            direction = 1;
	            state = State.Walking;
	        }
	      
	        else if (state != State.Talking)
	        {
	            state = State.Idle;
	        }
	    }


	    RunMovement();

	    Vector3 scale = transform.localScale;
	    scale.x = Mathf.Abs(scale.x)*direction;
	    transform.localScale = scale;

	    RunAnimation();



	}


    private void Jump()
    {
        frame = 0;
        frameCounter = 0;
        direction = 1;
        hasJumped = false;
        state = State.Jumping;
        yi = 0;
        xi = 0;
        ChangeFrame();
    }

    public override bool LeftDown
    {
        get { 
            
            if(AI == false)
                return base.LeftDown;

            return false;
        }
    }

    public override bool RightDown
    {
        get {

            if (AI == false)
                return base.RightDown;

            return false;
        }
    }


    private static List<bool> leftInput = new List<bool>();
    private static List<bool> rightInput = new List<bool>();
    private static List<bool> upInput = new List<bool>(); 

    private void ApplyFallingGravity()
    {
        yi -= Time.deltaTime * gravity;

    }


    private float angle = 0;

    private void ConstrainToGround( Vector3 movement)
    {
        float rayDist = Mathf.Abs(movement.y) * 1.2f;
        float bias = 200;

        float dist = movement.magnitude;
        Vector3 newPos = transform.position + movement;

        Vector3 origin = transform.position + movement + Vector3.up * bias;
        Vector3 ray =-transform.up;
        float d = bias + rayDist;
        RaycastHit2D hit = Physics2D.Raycast(origin, ray, d);
        Debug.DrawRay(origin, ray* d, Color.yellow);

        if (hit.collider != null)
        {
            Vector3 dir = (Vector3)hit.point - transform.position;

            angle = Extensions.Angle360(Vector3.up, hit.normal);
            if (hit.point.y >= newPos.y)
            {
       
                if (dir.magnitude > movement.magnitude)
                    movement = dir.normalized * dist;
                else
                {
                    movement = dir;
                }
                if (yi <= 0)
                {
                    yi = 0;

                    if (onground == false)
                    {
                        Land();
                    }
                }
            }
            else
            {
             //   movement = dir;

         //       Debug.DrawLine(hit.point, transform.position, Color.cyan);
                onground = false;
            }
        }
        else
        {
            onground = false;
        }
        transform.position += movement;
   
    }

   
    private bool onground = false;
    private void Land()
    {

        print("land");
        onground = true;
        yi = 0;
        if (state != State.Sliding && (state != State.Jumping || hasJumped))
        {

            state = State.Idle;
            print("--------------------- land " + state);

            ChangeFrame();
        }
    }

    private int frame;
    public float frameRate = 1/4f;
    private float frameCounter;
    public void RunAnimation()
    {

        frameCounter += Time.fixedDeltaTime;

        while (frameCounter > frameRate)
        {
            frameCounter -= frameRate;
            frame++;
            ChangeFrame();
        }

    }


    private void ChangeFrame()
    {

        switch (state)
        {
            case State.Idle:
                RunIdleAnimation();
                break;

            case State.Walking:
                RunWalkAnimation();
                break;

            case State.Jumping:
                RunJumpingAnimation();
                break;


            case State.Sliding:
                RunSlideAnimation();
                break;

            case State.Talking:
                RunTalkngAnimation();
                break;
        }
    }

    private void RunMovement()
    {

        switch (state)
        {
            case State.Idle:
                RunIdleMovement();
                break;

            case State.Walking:
                RunWalkingMovement();
                break;

            case State.Jumping:
                RunJumpingMovement();
                break;

            case State.Sliding:
                RunSlidingMovement();
                break;


          default:
                RunIdleMovement();
                break;
        }




        float speedM = 1;

        if (onground)
            speedM = 2;

        if (onground)
        {
 
        }
        if (xi < -0)
            xi = -0;

        if (yi < -maxFallspeed)
            yi = -maxFallspeed;


        Debug.DrawRay(transform.position, Vector3.up * yi, Color.red, 10);
        Debug.DrawRay(transform.position, Vector3.right * xi, Color.red, 10);

        Vector3 movement = Vector3.right * xi + Vector3.up * yi;

        ConstrainToGround(movement);

    }


    private void RunIdleMovement()
    {

        ApplyFallingGravity();
        float friction = -drag * xi * Time.fixedDeltaTime;
        xi +=  friction*3;
    }



    private void RunWalkingMovement()
    {
        ApplyFallingGravity();

        float max = maxSpeed;

        float acceleration = Time.fixedDeltaTime * moveAccel * direction;
        if (atEnd && AI)
        {
            max *= 1.5f;
            acceleration*=2;
        }
        float friction = -drag * xi * Time.fixedDeltaTime;
        xi += acceleration + friction;


      
        if (xi > max)
            xi = max;


    }


    private void RunSlidingMovement()
    {

        ApplyFallingGravity();
        ApplyFallingGravity();
        float acceleration = Time.deltaTime * moveAccel * direction*0.4f;
      //  acceleration *= angle/
        float friction = -drag * xi * Time.fixedDeltaTime*0.3f;

        if (angle > 20 && angle < 90)
            friction = 0;
        else
        {
            acceleration = 0;
        }
        xi += acceleration + friction;

        if (xi > maxSpeed*4)
            xi = maxSpeed *4;

    }

    private void RunJumpingMovement()
    {

        if (frame > 1)
            ApplyFallingGravity();
      
    }

    private bool hasJumped = false;
    private void RunJumpingAnimation()
    {

        frame = Mathf.Clamp(frame, 0, 2);
        if (frame == 0)
        {
            frameCounter -= 0.1f;
        }

        if (frame == 1)
        {
            if (hasJumped == false)
            {
                hasJumped = true;
                yi = jumpStrength;
                xi = jumpStrength;
                onground = false;
                print("---");
            }
        }
        if (frame == 2)
        {
           
        }
        sprite.SetLowerLeftPixel(frame * sprite.width, sprite.height*3);
    }


    private void RunIdleAnimation()
    {
        frame = 0;
        sprite.SetLowerLeftPixel(frame*sprite.width, sprite.height);
    }


    private void RunSlideAnimation()
    {
        frame = 0;
        sprite.SetLowerLeftPixel(frame * sprite.width, sprite.height*4);
    }

    private void RunWalkAnimation()
    {
        frame %= 2;
        sprite.SetLowerLeftPixel(frame * sprite.width, sprite.height*2);
    }


    private void RunTalkngAnimation()
    {

        //frameCounter -= 0.1f;
        int actualFrame = frame;


        if (talkGesture == 0)
        {
            if (frame < 6)
            {
            }
            else
            {
                state = State.Idle;
                //   actualFrame = 2;
            }
            actualFrame = frame%2;
        }
        else if (talkGesture == 1)
        {

             if (frame > 4)
            {
                state = State.Idle;
            }
              if (frame > 0)
            {
                actualFrame = 1;
            }
         
            else
            {
            }

            print(state + " " + frame);
        }
        else if (talkGesture == 2)
        {
             if (frame > 8)
            {
                state = State.Idle;
            }
              if (frame > 0)
              {
                  actualFrame = frame%2 + 1;
            }
         
        }

        sprite.SetLowerLeftPixel(actualFrame * sprite.width, sprite.height * (5 + talkGesture));
    }

    //private void OnGUI()
    //{
    //    GUI.color = Color.black;
    //    GUILayout.Label("" + angle);
    //    GUILayout.Label("" + state);
    //    GUILayout.Label("" + xi);
    //}
}
