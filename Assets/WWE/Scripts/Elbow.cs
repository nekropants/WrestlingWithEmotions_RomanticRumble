using UnityEngine;
using System.Collections;

public class Elbow : MonoBehaviour
{

    public Transform upperArm;
    public Transform lowerArm;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	    transform.rotation = Quaternion.Lerp(upperArm.rotation, lowerArm.rotation, 0.5f);

	}
}
