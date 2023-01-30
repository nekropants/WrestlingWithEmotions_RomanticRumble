using UnityEngine;
using System.Collections;

namespace Dance { 
 public class LimbTracker : MonoBehaviour
{
    private Vector3 prevPos;

	// Use this for initialization
	void Start ()
	{

	    prevPos = transform.position;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    Vector3 diff = transform.position - prevPos;

	    DanceEvaluator.limbMovement += diff.magnitude;
	    prevPos = transform.position;

        
	}
}

}