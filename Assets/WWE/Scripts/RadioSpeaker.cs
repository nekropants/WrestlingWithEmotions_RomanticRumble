using UnityEngine;
using System.Collections;

public class RadioSpeaker : MonoBehaviour
{
    public float amplitude = 1;
    public float freqeuncy = 1;

    public AnimationCurve animationCurve;

    private Vector3 offset;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

	    transform.localScale -= offset;

	    offset = animationCurve.Evaluate(Time.time*freqeuncy)*amplitude * Vector3.one;

        transform.localScale += offset;

    }
}
