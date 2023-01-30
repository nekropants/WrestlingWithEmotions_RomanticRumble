using UnityEngine;
using System.Collections;

public class MotionLines : MonoBehaviour
{
    public float speed = 1;
    public float random = 0.1f;

    // Use this for initialization
    void Start ()
    {
        speed += Random.value*speed*random;

    }
	
	// Update is called once per frame
	void Update ()
	{

	    transform.position += Vector3.up*Time.deltaTime*speed;

	}
}
