using UnityEngine;
using System.Collections;

namespace Dance { 
 public class ShowAvatar : MonoBehaviour
{

    public AnimationCurve yCurve;
    private float lerp;
    private Vector3 start;

    public float amplitude = 1f;
    public float frequency = 1;


    public bool showing = false;
    public bool hiding = false;
    // Use this for initialization
    void Start ()
	{
	    start = transform.localPosition;
        transform.localPosition = start + (yCurve.Evaluate(0) - 1) * amplitude * Vector3.up;
    }

    // Update is called once per frame
    void Update ()
	{

	    if (showing)
	    {
	        lerp += Time.deltaTime*frequency;
	        if (lerp >= 1)
	            showing = false;
	        lerp = Mathf.Clamp01(lerp);
	        transform.localPosition = start + (yCurve.Evaluate(lerp) - 1)*amplitude*Vector3.up;
        }

        if (hiding)
        {
            lerp -= Time.deltaTime * frequency*2;
            if (lerp <= 0)
                hiding = false;
	        lerp = Mathf.Clamp01(lerp);
            transform.localPosition = start + (lerp - 1)  * amplitude * Vector3.up;
        }
    }
}

}