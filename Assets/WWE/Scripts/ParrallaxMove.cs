using UnityEngine;
using System.Collections;

public class ParrallaxMove : MonoBehaviour
{
    public Vector3 direction;
    public float speed = 1;
    public float drag = 1;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    speed -= (speed)*drag * Time.deltaTime ;
	    transform.position += direction.normalized*speed*Time.deltaTime*PoseController .direction;
	}



}
