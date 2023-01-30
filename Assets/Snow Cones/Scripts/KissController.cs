using System.Timers;
using UnityEngine;
using System.Collections;

public class KissController : MonoBase
{

    public Transform cone1;
    public Transform cone2;
    public Transform kissEffects;

    private Vector3 start1;
    private Vector3 start2;

    private float closeness = 0;
    private float distance = 120;

	// Use this for initialization
	void Start () {

        start1 = cone1.transform.position;
        start2 = cone2.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float speed = 0.3f;
	    if (Right)
	        closeness += Time.deltaTime*speed;

	    if (Left)
	    {
            closeness -= Time.deltaTime*speed;
	    }

	    closeness = Mathf.Clamp01(closeness);

        cone1.transform.position = start1 - Vector3.right * closeness * distance * 0.8f;
        cone2.transform.position = start2 - Vector3.left * closeness* distance;


	    if (closeness > 0.9f)
	    {
	        kissEffects.gameObject.SetActive(true);
	    }
	}
}
