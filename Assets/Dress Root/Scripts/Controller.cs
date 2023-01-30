using UnityEngine;
using System.Collections;

namespace Dance { 
 public class Controller : MonoBehaviour
{


    public float timeSlow= 1;
    public Object[] spriteSheets;
    private AudioSource audio;

    //public bool slow = false;
    //public bool slow = false;
	// Use this for initialization
	void Start ()
	{
	    audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKey(KeyCode.Space))
	    {
	        timeSlow -= Time.deltaTime*3;

	    }
	    else
	    {
            timeSlow += Time.deltaTime*8;

        }

        timeSlow = Mathf.Clamp(timeSlow, 0.1f, 1);

	    Time.timeScale = timeSlow;


	}
}

}