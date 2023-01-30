using System;
using UnityEngine;
using System.Collections;

public class MapCones : MonoBehaviour
{


    public float timeOffset = 0;

	// Use this for initialization
	void Start () {
	
	}


    private float timer = 0;

    public float frequency = 5;
    public float amplitude = 5;

    private Vector3 offset;
	// Update is called once per frame
	void Update ()
	{

	    timer += Time.deltaTime;

	    transform.position -= offset;
        float sin = Mathf.Abs(Mathf.Sin(timeOffset + timer * frequency) * amplitude);
        offset = new Vector3(0, sin, 0);
        transform.position += offset;

	}
}
