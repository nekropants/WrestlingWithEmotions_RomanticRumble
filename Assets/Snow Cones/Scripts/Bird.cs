using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour
{


    public float ySpeedM;
    public AnimationCurve ySpeedCurve;
    public float XSpeed = 10;
    private Animator animator;
    private float lerp = 0;

    private float rand = 1;

    public static List<Bird> birds = new List<Bird>();

    public bool released = false;

	// Use this for initialization
	void Start ()
	{
	    animator = GetComponent<Animator>();
        rand = Random.Range(0.9f, 1.1f);
        animator.speed = rand;
	    birds.Add(this);
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (released == false)
            return;

        lerp += Time.deltaTime * 0.2f * rand;
        transform.position += XSpeed * Time.deltaTime * Vector3.left * rand;
        transform.position += ySpeedCurve.Evaluate(lerp) * Time.deltaTime * Vector3.up * ySpeedM;
	}
}
