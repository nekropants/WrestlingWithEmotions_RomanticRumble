using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SetZ : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	    transform.SetLocalZ(transform.localPosition.y*0.4f);

	}
}
