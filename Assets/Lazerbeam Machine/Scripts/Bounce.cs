using UnityEngine;
using System.Collections;

public class Bounce : MonoBehaviour
{

    public float frequency =1;
    public float Amplitude =1 ;
    public float offset = Mathf.PI*2;

    public float rotM = 1;

public bool useScale = false;
    float defaulScale =1;
    // Use this for initialization
    void Start () {
	    defaulScale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update ()
	{

	    float sin = Mathf.Sin(frequency*Time.time + offset);

        transform.localScale = Vector3.one*(defaulScale + sin * Amplitude);
	    transform.rotation = Quaternion.Euler(0, 0, sin*rotM);

	}
}
