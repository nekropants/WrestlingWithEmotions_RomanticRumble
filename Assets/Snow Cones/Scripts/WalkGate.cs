using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using System.Collections;

public class WalkGate : MonoBase
{
    public SpriteRenderer sprite;
    public SpriteSM trail;
    private float walkDist = 360;
    private Vector3 start;
    public bool playerControlled = false;

    private float displacement = 0;


    public AudioClip[] footSteps;
    public int footStepsIndex = 0;

	// Use this for initialization
	void Start ()
	{
	    start = transform.position;
	    trail.width = 0;
	    coneDates.Add(this);
	    trail.transform.parent = transform.parent;
	    sinTimer = 1 + Random.value*Mathf.PI;

	    footStepsIndex = Random.Range(0, 5);
	}

    private float speed = 15;

    private float sinTimer = 0;
    private static List<WalkGate> coneDates = new List<WalkGate>(); 
	// Update is called once per frame

	void Update () 
    {
	    if (playerControlled == false)
	    {

	        if (displacement < walkDist)
	        {
	            displacement += Time.deltaTime*speed;
                sinTimer += Time.deltaTime * 10;
	        }
	        if (displacement >= walkDist)
	        {
	            DestinationReached();
	        }
            displacement = Mathf.Clamp(displacement, 0, walkDist);
        }
	    else
	    {

            if (Right || Touch || displacement < 40)
	        {
	            displacement += Time.deltaTime*speed*1.3f;
                sinTimer += Time.deltaTime * 10;
	        }
	        if (Left)
	        {
	            displacement -= Time.deltaTime*speed*1.3f;
                sinTimer += Time.deltaTime * 10;
            }
	        displacement = Mathf.Clamp(displacement, 0, walkDist*2);

	        float dist = (coneDates[0].transform.position - coneDates[1].transform.position).magnitude;

	        if (dist < 30)
	        {
	            DestinationReached();
                SceneController.ChangeScene(SceneEnum.ParkGateMeetAwkwardHug);
	        }

	    }
	    int dir = 1;
        if (playerControlled)
            dir = -1;

	    if (Mathf.Abs(trail.width) < Mathf.Abs(displacement))
	    {
	        trail.width = -displacement*dir;
            trail.pixelDimensions.x = trail.width;
	    }
	    trail.RefreshVertices();
        trail.CalcUVs();
        trail.UpdateUVs();

        Vector3 pos = start + dir*Vector3.left * displacement;
	    transform.position = pos;

	    if (sinTimer > Mathf.PI*2)
	    {
	        sinTimer -= Mathf.PI*2;

	        float volume = 0.2f;

            if (playerControlled == false)
	             volume = 0.05f;
            
	        
	            footStepsIndex %= footSteps.Length;
                AudioControllerShit.Play(footSteps[footStepsIndex], Random.Range(0.9f, 1.1f), volume);
	            footStepsIndex++;

	    }

	    float offset = Mathf.Sin(sinTimer );
        
        sprite.transform.localPosition = Vector3.up *offset* 0.3f;
	}
    
    private void DestinationReached()
    {
        sprite.transform.transform.rotation = Quaternion.identity;

    }

    void OnDestroy()
    {
        coneDates.Remove(this);
    }
}
