using UnityEngine;
using System.Collections;

namespace Dance { 
 public class RandomShake : MonoBehaviour {

    Vector3 offset = Vector3.zero;
    public float range = 0.1f;
    public float freq = 0.1f;

    private float timer = 0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	    timer += Time.deltaTime;

	    if (timer > freq)
	    {
	        timer -= freq;

	        transform.localPosition -= offset;
	        offset = Random.insideUnitCircle*range;
	        transform.localPosition += offset;
        }

	}
}

}