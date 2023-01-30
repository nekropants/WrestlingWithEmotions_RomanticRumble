using UnityEngine;
using System.Collections;

public class ArenaWrestler : MonoBehaviour
{
    public bool isOpponent = false;
    private int frame = 0;
    private float frameRate = 1/3f;
    private float timer = 0;

    public float defaultSpeed = 2;
    public float speed = 1;

    public Sprite[] walkFrames;
    public Sprite[] runFrames;
    public Sprite[] idleFrames;
    public Sprite[] climbRopeFrames;
    public Sprite[] standinOnRopesFrames;
    public Sprite[] jumpOffRopeFrames;
    public Sprite[] elbowOntoGround;
    public Sprite[] knockedOnBack;
    public Sprite[] againstRopes;
    public Sprite[] liftFrames;
    public Sprite[] getLiftedFrames;
    public Sprite[] flyKickFrames;

    private SpriteRenderer spriteRenderer;

    public CircleCollider2D getOnRopes;

    private float lastWalkInputTime = 0;
    private Vector3 prevMousePos;

    public bool climbRopes = false;
    public bool standingOnRopes = false;
    public bool againstRope = false;
    public bool liftingPlayer = false;
    public int ropeDirection = 0;
    public bool onBack = false;


    private Vector3 target;

    public static ArenaWrestler player;
    public static ArenaWrestler opponent;

    float onBackTimer = 0;

    public static float DistanceBetweenPlayers
    {

        get
        {
            return (player.transform.position - opponent.transform.position).magnitude;

        }
    }

    public bool Incapacitated
    {
        get
        {
            return paused || onBack;
        }
    }

    public ArenaWrestler otherPlayer
    {
        get
        {
            if (isOpponent)
            {
                return player;
            }
            else
            {
                return opponent;
            }
        }
    }

    public int SignedDirection
    {
        get
        {
            return (int)Mathf.Sign(direction.x);
        }
    }

    // Use this for initialization
    void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        prevMousePos = Input.mousePosition;
        target = transform.position;

        if (isOpponent)
        {
            opponent = this;
        }
        else
        {
            player = this;
        }

    }
    bool bouncingOffRopes = false;
    bool stopIfNotDoubleClick = false;
    float lastClickUp = 0;
	    Vector3 direction = Vector3.zero;

    float clickDistFromPlayer = 0;
   // Update is called once per frame
   void Update ()
    {

        if(Paused)
            return;

        if (isOpponent == false)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (Time.timeScale == 0.05f)
                    Time.timeScale = 1;
                else
                {
                    Time.timeScale = 0.05f;
                }
            }
        }

        RunAnimation();

        if (!climbRopes && !jumpingOffRopes && !standingOnRopes && !liftingPlayer)
        {
            if (getOnRopes.OverlapPoint(transform.position))
            {
                climbRopes = true;
                SetTarget(getOnRopes.transform.position);
                ChangeAnimation();
            }

            if (Input.GetKeyDown(KeyCode.R) && isOpponent == false)
            {

                StartCoroutine(LiftOtherPlayer());                                                                                                                                                               
            }
        }


        if(onBack)
        {
            onBackTimer += Time.deltaTime;
            if(onBackTimer > 4)
            {
                onBack = false;
                Audience.instance.StopCheering ();

            }
        }

        //if(Input.GetKeyUp(KeyCode.Mouse0))
        //{
        //    if (bouncingOffRopes == false)
        //    {


        //        lastClickUp = Time.time;
        //        stopIfNotDoubleClick = !stopIfNotDoubleClick;
        //    }
        //    else
        //    {
        //        bouncingOffRopes = false;
        //    }
        //}

        //if(stopIfNotDoubleClick)
        //{
        //    if(Time.time - lastClickUp > 0.1f)
        //    {
        //        stopIfNotDoubleClick = false;


        //        target = transform.position;
        //        direction.z = 0;
        //    }
        //}
        

        Vector3 delta = Input.mousePosition - prevMousePos;
        prevMousePos = Input.mousePosition;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.down;
        bool clickedBehindPlayer = (Mathf.Sign(mousePos.x - transform.position.x) *SignedDirection < 0 );


        Vector3 diff = mousePos - (transform .position + Vector3.up);
        diff.z = 0;
         clickDistFromPlayer = Vector3.Magnitude(diff);

        if (MouseDown && isOpponent == false && flyKicking ==false)
        {
            bool wasRunning = running;
           
            running = (Time.time - lastMouseDown) < 0.25f;
            lastMouseDown = Time.time;
            print(running + " " + wasRunning);

            if(running == false && (clickDistFromPlayer < 1f || DistanceBetweenPlayers < 2) && opponent.Incapacitated == false)
            {
                StartCoroutine(LiftOtherPlayer());

            }
            else   if (againstRope)
            {
                againstRope = false;
                target = transform.position- Vector3.right * ropeDirection*10;
                running = true;
                bouncingOffRopes = true;
            }
            else if (wasRunning && clickedBehindPlayer == false)
            {
                StartCoroutine(FlyKickRoutine());

            }
            
            else
            {

                if (wasRunning) // stop running on click
                {
                    running = false;
                    SetTarget(transform.position); 
                }
                 else    if (!climbRopes && !jumpingOffRopes && !standingOnRopes && !liftingPlayer)
                {

                    SetTarget(mousePos);
                }

                if (running && !wasRunning)
                {
                    target += direction.normalized * 10;
                }

                if (standingOnRopes)
                {
                    standingOnRopes = false;
                    jumpingOffRopes = true;
                    StartCoroutine(JumpOffRopes());
                }
            }
        }
        MoveToTarget();


        if (againstRope)
        {
            
            transform.localScale = new Vector3(ropeDirection, 1, 1);
        }
        else if (direction != Vector3.zero)
        {
            transform.localScale = new Vector3(-Mathf.Sign(direction.x), 1, 1);
        }


        againstRope = false;

    }

    private float lastMouseDown = 0;


    bool MouseDown
    {
        get { return Input.GetKeyDown(KeyCode.Mouse0) && PoseController.open == false; }
    }


    private void OnDrawGizmos()
    {
        Debug.DrawRay(target, Random.onUnitSphere, Color.red);
    }

    void SetTarget( Vector3 newTarget)
    {
      
        target = newTarget;
        target.z = transform.position.z;
  
    }


    void SetPositionAndTarget(Vector3 position)
    {
        target = position;
        transform.position = target;
    }



    void LookTowards(Vector3 point)
    {
        transform.localScale = new Vector3(Mathf.Sign(transform.position.x - point.x), 1, 1);
    }

    void SetDirection(int newDir)
    {
        transform.localScale= new Vector3(Mathf.Sign(newDir), 1, 1);
    }

    public void KnockBack(int dir)
    {
        if (onBack)
            return;

        StartCoroutine(KnockBackRoutine(dir));
    }

    public IEnumerator  KnockBackRoutine( int dir)
    {
        KnockOnBack();
        PoseController.FlyKick (-dir);

        while (Paused)
        {
            yield return null;

        }
        SetTarget(transform.position + Vector3.right * 3 * dir);


        float lerp = 1;
        while(lerp > 0)
        {
            lerp -= Time.deltaTime*2;
            lerp = Mathf.Clamp01(lerp);

            offset = Vector3.up * lerp*1.5f;
            yield return null;
        }


    }
    public void KnockOnBack()
    {
        Audience.instance.Cheer();
        onBackTimer = 0;
        onBack = true;
        ChangeFrame();
    }


    Vector3 prevOffset;
    Vector3 offset;
    void MoveToTarget()
    {
        transform.position -= prevOffset;


        float displacement = Time.deltaTime;

        print("MoveToTarget " +  this);
        
        if (jumpingOffRopes)
            displacement *= 12;
        else if(flyKicking)
        {
            displacement *= 8;
        }
        else if (onBack)
        {
            displacement *= 9;
        }
        else
        {
            displacement *= 2;

            if (running  )
                displacement *= 3;
        }


        if (target != transform.position)
        {
            Vector3 diff = target - transform.position;

            if (diff.magnitude > displacement)
                diff = diff.normalized * displacement;


            direction = diff;
            
            lastWalkInputTime = Time.time;
        }
        else
        {
            flyKicking = false;
            running = false;
            direction = Vector3.zero;
        }

        RaycastHit hit;


            Physics.Raycast(transform.position - direction.normalized, direction.normalized, out hit, direction.magnitude + 1);

        if (hit.collider && hit.distance < 1)
        {

            print("hit " + hit.collider);
            //   direction = (Vector3)hit.point - transform.position - direction.normalized * 0.01f;
            //   target = transform.position + direction;
            //    SetTarget(transform.position + direction);

            // direction = Vector3.zero;
            flyKicking = false;
            running  = false;
            direction.x = -direction.x;
            transform.position = hit.point;
            target = transform.position;
            direction.z = 0;
        }
        else
        {
        transform.position = transform.position + direction;
        }
        //   if (Input.GetKey(KeyCode.A))
        //{
        //    direction.x--;
        //}
        //   if (Input.GetKey(KeyCode.D))
        //   {
        //       direction.x++;
        //   }
        print( this + " " +offset);
        transform.position += offset;
        prevOffset = offset;
    }

    public AnimationCurve slamCurve;

    IEnumerator FlyKickRoutine()
    {

        flyKicking = true;
        SetTarget(  transform.position + Mathf.Sign (direction.x) * Vector3.right*6);
        Debug.DrawRay(target, Random.onUnitSphere, Color.red, 10);
        Debug.DrawRay(target, Random.onUnitSphere, Color.red, 10);
        ResetFrame();


        while(flyKicking)
        {
            yield return null;
            if(DistanceBetweenPlayers < 1)
            {
                otherPlayer.KnockBack( SignedDirection );
            }
        }
        //float timer = 1;
        //while(timer > 0)
        //{
        //    timer -= Time.deltaTime*2;
        //    timer = Mathf.Clamp01(timer);
        //    offset = Vector3.up * timer* 1.5f;
        //    print(offset);
        //    yield return null;
        //}
        yield return null;
    }

    void ResetFrame()
    {
        frame = 0;
        timer = 0;
        ChangeFrame();
    }
    public Coroutine SlamOnGround()
    {
       return   StartCoroutine(SlamOnGroundRoutine());
    }

    IEnumerator SlamOnGroundRoutine()
    {
        float t = 0;


        Vector3 _offset = Vector3.zero;
        while (t < 1)
        {

            
            yield return null;

            t += Time.deltaTime;
            t = Mathf.Clamp01(t);

            transform.position -= _offset;
            _offset = Vector3.up*slamCurve.Evaluate(t);
            print(offset);
            transform.position += _offset;
        }


        SetTarget(transform.position);

        KnockOnBack();
    }

    private bool running = false;
    private bool jumpingOffRopes = false;
    IEnumerator JumpOffRopes()
    {
        jumpingOffRopes = true;
        climbRopes = false;
        ChangeAnimation();

        Vector3 newTarget = transform.position + new Vector3(-2.5f, -1, 0);
        if (Vector3.Distance(transform.position, otherPlayer.transform.position) < 5)
        {
            newTarget = otherPlayer.transform.position;
        }
        SetTarget(newTarget);


        bool hitPlayer = DistanceBetweenPlayers < 3;

        print(DistanceBetweenPlayers);
   
        if(hitPlayer)
        {

            while (true)
            {
                yield return null;

                if (ReachedTarget(1.6f))
                {

                    print(DistanceBetweenPlayers);

                    PoseController.PeopleElbow();
                    while (Paused)
                    {
                        yield return null;

                    }
                    break;

                }



            }
        }
        while (true)
        {
            yield return null;

            if (ReachedTarget())
            {
                break;
            }
        }

        if(hitPlayer)
        otherPlayer.KnockOnBack();

        while (true)
        {
            yield return null;

            if (MouseDown)
            {
                break;
            }
        }
        //yield return new WaitForSeconds(1.2f);
        jumpingOffRopes = false;

    }

    IEnumerator LiftOtherPlayer()
    {
        liftingPlayer = true;

        spriteRenderer.sprite = liftFrames[0];

        LookTowards(opponent.transform.position);

        if (  DistanceBetweenPlayers > 2)
        {
            //if(target == transform.position)
                paused = true;
                yield return new WaitForSeconds(.2f);
                paused = false;
            liftingPlayer = false;

            yield break;

        }


        otherPlayer.paused = true;

        otherPlayer.LookTowards(transform.position);
        SetPositionAndTarget(transform.position);


        otherPlayer.spriteRenderer.sprite = getLiftedFrames[0];

        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }


        Audience.instance.Cheer();
        spriteRenderer.sprite = liftFrames[1];
        otherPlayer.spriteRenderer.sprite = getLiftedFrames[1];
        otherPlayer.SetPositionAndTarget(transform.position + Vector3.up * 1.5f );

       // yield return new WaitForSeconds(0.6f);
        yield return null;

        while (Input.GetKeyDown(KeyCode.Mouse0) == false)
        {
            yield return null;

        }

        PoseController.Lift();


        while (PoseController.open)
        {
            yield return null;

        }
        liftingPlayer = false;

        yield return otherPlayer.SlamOnGround();
        otherPlayer.paused = false;


        yield return new WaitForSeconds(1);
        Audience.instance.StopCheering();

    }


    bool flyKicking = false;
    private bool paused = false;

    bool Paused
    {
        get { return paused || PoseController.open; }
    }

    bool ReachedTarget( float minDistance = 0.01f)
    {
        return Vector3.Distance(transform.position, target) < minDistance;
    }
    void RunAnimation()
    {
      //  frameRate = Mathf.Max(frameRate, 0.001f);
        timer += Time.deltaTime;

        while (timer > frameRate)
        {
            timer -= frameRate;
            frame++;

        }

        ChangeFrame();
    }

    void ChangeAnimation()
    {
        frame = 0;
        ChangeFrame();
    }

    void ChangeFrame()
    {

        if (onBack)
        {
            frame = 0;
            spriteRenderer.sprite = knockedOnBack[frame];
        }
        else if(flyKicking)
        {
            frameRate = 1 / 6f;
            frame = Mathf.Min(frame, flyKickFrames.Length - 1);
            //frame = 0;
            spriteRenderer.sprite = flyKickFrames[frame];
        }
        else if (standingOnRopes)
        {
            frameRate = 1 / 10f;
            frame %= standinOnRopesFrames.Length;
            spriteRenderer.sprite = standinOnRopesFrames[frame];
        }
        else
      if (jumpingOffRopes)
        {
            frameRate = 1 / 10f;
            frame = Mathf.Min(frame,  jumpOffRopeFrames.Length -1);

            if(ReachedTarget() && frame > 2)
                spriteRenderer.sprite = elbowOntoGround[0];
            else
            {
                spriteRenderer.sprite = jumpOffRopeFrames[frame];
            }
        }
        else if (climbRopes)
        {
            frameRate = 1/10f;

            if (frame >= climbRopeFrames.Length)
            {
                climbRopes = false;
                standingOnRopes = true;
                ChangeAnimation();
            }
            else
            {
                spriteRenderer.sprite = climbRopeFrames[frame];

            }
        }
        else if (liftingPlayer)
        {
         

        }
        else if (againstRope && !bouncingOffRopes)
        {
            frameRate = 1 / 6f;
            frame %= againstRopes.Length;
            spriteRenderer.sprite = againstRopes[frame];

        }

        else if (direction != Vector3.zero)
        {
            if (running)
            {
                frameRate = 1 / 6f;
                frame %= runFrames.Length;
                spriteRenderer.sprite = runFrames[frame];
            }
            else
            {

                frameRate = 1 / 3f;
                frame %= walkFrames.Length;
                spriteRenderer.sprite = walkFrames[frame];
            }
        }

        else
        {
            frameRate = 1 / 3f;
            frame %= idleFrames.Length;
            spriteRenderer.sprite = idleFrames[frame];
        }



        //climbRopeFrames
    }
}
