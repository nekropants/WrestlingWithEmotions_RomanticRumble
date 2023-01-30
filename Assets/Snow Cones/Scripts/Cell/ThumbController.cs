using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;

public class ThumbController : MonoBase
{
    public Transform targetButton;

    private Vector3 idlePos;
    private Vector3 pos;
    private float z;


    public AudioClip[] buttonSounds;
    private int soundIndex = 0;
	// Use this for initialization
	void Start ()
	{
        pos = transform.transform.position;
        idlePos = pos;
	    z = pos.z;
	}

    public void SetTarget(Transform target)
    {
      //  CellShake.Shake();
        targetButton = target;
    }

    // Update is called once per frame
	void Update () {
        float speed = 1000;

        Vector3 direction = Vector3.zero;

	    if (targetButton == null)
	    {
	        pos = Vector3.Lerp(pos, idlePos, Time.deltaTime*5);
	    }
	    else
	    {

            Vector3 d = pos - targetButton.position;
	        d.z = 0;
            if (d.magnitude < 20)
	        {
	            targetButton = null;
	            soundIndex++;
	            soundIndex %= buttonSounds.Length;
                AudioControllerShit.Play(buttonSounds[soundIndex], Random.Range(0.9f, 1.1f), 0.2f);

	        }
	        else
	        {
                pos = Vector3.Lerp(pos, targetButton.position, Time.deltaTime * 20);
	            
	        }
	    }

	    float maxDist = 700;
        Vector3 diff =  transform.localPosition;
        if (diff.magnitude > maxDist)
        {
            // transform.transform.localPosition = diff.normalized * maxDist;
            //pos = transform.position;
        }

	    pos.z = z;
        transform.position = pos;
    }
}
