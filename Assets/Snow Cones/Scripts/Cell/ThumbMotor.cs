using UnityEngine;
using System.Collections;

public class ThumbMotor : MonoBehaviour
{
    private Rigidbody2D rBody;
    public float force = 5;
	// Use this for initialization
	void Start ()
	{
        rBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    rBody.AddTorque(force);
	}
}
