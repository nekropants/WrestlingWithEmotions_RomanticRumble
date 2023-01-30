//using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;

public class ApproachCone : MonoBase
{

    public bool ai = false;
    public float speed = 10;
    private Vector3 anchor;

    public float freq = 0;
    public float amplitude = 0;
    private float sinTimer = Mathf.PI*2;
    private float offset = 0;
    public float maxDist = 100;

    public Animator animator;
    // Use this for initialization
    private void Start()
    {
        anchor = transform.position;
        if (ai == false)
        {
            animator.Play("ApproachAnimation");
            animator.speed = 0;
        }
    }

    private bool BirdsRelease = false;

    // Update is called once per frame
    private void Update()
    {
        float movement = 0;

        if (offset < maxDist)
        {
            if (ai)
                movement = 1;

            if (Up || Touch)
            {
                movement = 1;
            }

        }


        if (ai == false)
        {
            if (offset > 0)
                if (Down)
                {
                 //   movement = -1;
                }

            if (offset > maxDist/2f)
            {
                if (BirdsRelease == false)
                {
                    StartCoroutine(ReleaseBirds());
                    BirdsRelease = true;
                }
            }

            if (offset >= maxDist && hopped == false && (Left || TouchDown ))
            {
                hopped = true;
                StartCoroutine(Moveleft());
            }


        }

        {
            offset = Mathf.Clamp(offset, 0, maxDist);
            offset += movement*speed*Time.deltaTime;
        }

        //  offset = Mathf.Clamp(offset, 0, maxDist);
        if (sinTimer < Mathf.PI*2)
        {
            sinTimer += Time.deltaTime*freq;
        }
        else if (movement != 0)
        {
            sinTimer = 0;
        }

        sinTimer = Mathf.Clamp(sinTimer, 0, Mathf.PI*2);


        float sin = Mathf.Sin(sinTimer)*amplitude;
        if (hopped == false)
            transform.position = anchor + (offset + sin) * Vector3.up;

        //       Vector3 

    }

    private IEnumerator ReleaseBirds()
    {
        yield return new WaitForSeconds(1.1f);
        foreach (Bird bird in Bird.birds)
        {
            yield return new WaitForSeconds(0.1f);
            bird.released = true;
        }
    }

    private bool hopped = false;
    private IEnumerator Moveleft()
    {
        Vector3 anchor = transform.position;


        float lerp = 0;
        float speed =2f;
        while (lerp < 1 )
        {
            lerp += Time.deltaTime * 1.6f * speed;
            lerp = Mathf.Clamp01(lerp);

            transform.position = anchor + Vector3.left * lerp * 12 + Vector3.up * 8 * (SinHop(lerp * 1 ));
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        anchor = transform.position;
        lerp = 0;
        while (lerp < 1)
        {
            lerp += Time.deltaTime * 1.6f * speed;
            lerp = Mathf.Clamp01(lerp);

            transform.position = anchor + Vector3.left * lerp * 12 + Vector3.up * 8 * (SinHop(lerp * 1 ));
            yield return null;
        }

        yield return new WaitForSeconds(6f);
        animator.Play("ApproachAnimation");
        animator.speed = 1f;
    }

    private float SinHop(float t )
    {

        return Mathf.Abs( Mathf.Sin(t*Mathf.PI));
    }

    IEnumerator  PlayAnim()
    {
        yield return new WaitForSeconds(6f);
     
    }
}
