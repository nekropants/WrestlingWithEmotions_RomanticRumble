using UnityEngine;
using System.Collections;

public class DinoCam : MonoBehaviour
{


    public float maxHorizontal;
    public float maxVertical;

    public Transform player;
    private Vector3 target;
    private Vector3 start;
	// Use this for initialization
	void Start ()
	{
        start = transform.position;
	    target = start;
	}
	
	// Update is called once per frame
	void LateUpdate ()
	{

        target.x = Mathf.Max(target.x, player.transform.position.x + maxHorizontal);

	  //  if (target.y > player.transform.position.y + maxVertical)
	    {
	        target.y = player.transform.position.y - maxVertical;
	    }
	    target.y = Mathf.Max(target.y, start.y);
	    transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime*5f);
	}
}
