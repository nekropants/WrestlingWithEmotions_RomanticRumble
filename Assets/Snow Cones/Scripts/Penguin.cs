using UnityEngine;
using System.Collections;

public class Penguin : MonoBehaviour
{

    private SpriteSM sprite;
    private Rigidbody rBody;

    private bool OnLake = false;
	// Use this for initialization
	void Start ()
	{
	    sprite = GetComponent<SpriteSM>();
        rBody = GetComponent<Rigidbody>();
	}

    private bool gliding = false;
	// Update is called once per frame
	void Update ()
	{
	    if (OnLake == false)
	    {
	        AnimateWalking();
            rBody.velocity = -transform.right * 100;
            gliding = false;
        }
	    else
	    {
	        AnimateGliding();

	        if (gliding == false)
	        {
	            rBody.velocity = -transform.right*500;
                rBody.angularVelocity = Vector3.forward * 0.0f;
                gliding = true;
	        }
	    }

	}


    private void FixedUpdate()
    {
        OnLake = false;
    }

    private float walkTimer = 0;
    private float walkFrameRate = 0.2f;
    private int walkFrame= 0;

    private void AnimateWalking()
    {
        walkTimer += Time.deltaTime;
        while (walkTimer > walkFrameRate)
        {
            walkTimer -= walkFrameRate;
            walkFrame++;
        }

        walkFrame %= 2;
        sprite.SetLowerLeftPixel(walkFrame*256, 256);
    }

    private void AnimateGliding()
    {
        walkTimer += Time.deltaTime;
        while (walkTimer > walkFrameRate)
        {
            walkTimer -= walkFrameRate;
            walkFrame++;
        }

        walkFrame %= 2;
        sprite.SetLowerLeftPixel( 256, 512);
    }


    void OnTriggerStay(Collider other)
    {
        OnLake = true;
    }
}
