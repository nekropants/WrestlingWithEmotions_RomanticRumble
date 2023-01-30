using UnityEngine;
using System.Collections;

public class PingPongRotate : MonoBehaviour
{

    public float frequency = 2;
    public float amplitude = 5;

    private Quaternion offset = Quaternion.identity;


    public int direction = 1;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	    transform.rotation *= Quaternion.Inverse(offset);
        float sin = Mathf.Sin(Time.time * frequency) * amplitude * direction;
	    offset = Quaternion.AngleAxis(sin, Vector3.forward);
        transform.rotation *= offset;


	}
}
