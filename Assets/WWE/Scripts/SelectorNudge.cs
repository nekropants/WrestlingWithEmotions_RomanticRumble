using UnityEngine;
using System.Collections;

public class SelectorNudge : MonoBehaviour
{
    private int direction = 1;
    public static SelectorNudge instance;
    private float timer;
    private float sintimer;
    public float freq = 1;
    public float amp = 1;


    private Vector3 offset;
    // Use this for initialization
    void Start ()
	{
	    instance = this;
	}
	
	// Update is called once per frame
	void Update ()
	{

	    transform.position -= offset;
        sintimer += Time.deltaTime*direction;

	    float sin = Mathf.Sin(sintimer * freq)*amp;


        timer -= Time.deltaTime * 2;
        timer = Mathf.Clamp01(timer);

        offset = Vector3.right*sin* timer;

        transform.position += offset;

	  
	}


    public void Nudge(int _direction)
    {
        direction = _direction;
        timer = 1;
        sintimer = 0;
    }
}
