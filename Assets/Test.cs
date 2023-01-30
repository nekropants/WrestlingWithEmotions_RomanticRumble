using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour
{


    public float m = 20;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    GetComponent<Rigidbody2D>().AddForceAtPosition( Vector3.up*Time.deltaTime*m, -transform.right * 0.5f);
	}
}
