using UnityEngine;
using System.Collections;

namespace Dance { 
 public class MagicMoon : MonoBehaviour
{


    public AnimationCurve curve;

    public float freq = 1;
    public float amp = 1;

    private float timer = 0;
    // Use this for initialization

    private Vector3 offset;
	// Update is called once per frame
	void Update ()
	{
	    timer += Time.deltaTime*freq;

	    transform.localPosition -= offset;
        offset.y =  curve.Evaluate(timer)*amp;
        transform.localPosition += offset;

    }
}

}