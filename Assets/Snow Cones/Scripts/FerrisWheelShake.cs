using UnityEngine;
using System.Collections;

public class FerrisWheelShake : MonoBehaviour
{


    private Vector3 offset = Vector3.zero;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    transform.position -= offset;
	    offset = Mathf.Sin(Time.time)*Vector3.up*2;
        transform.position += offset;
    }
}
