using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{

    public static Audience instance;
    public  Animator animator;

    // Use this for initialization
    void Start ()
	{
	    instance = this;
	    animator = GetComponentInChildren<Animator>(true);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Cheer()
    {
        animator.enabled = true;
    }

    public void StopCheering()
    {
        animator.enabled = false;
    }
}
