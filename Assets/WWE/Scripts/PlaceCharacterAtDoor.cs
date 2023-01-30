using UnityEngine;
using System.Collections;

public class PlaceCharacterAtDoor : MonoBehaviour
{

    public static PlaceCharacterAtDoor instance;

    public GameObject playerAtMirror;
	// Use this for initialization
	void Start ()
	{
	    instance = this;

	    playerAtMirror.transform.parent = transform;
        playerAtMirror.transform.localPosition = Vector3.zero;
        playerAtMirror.transform.localScale = Vector3.one;
        GetComponent<SpriteRenderer>().enabled = false;

	}

    public void DoIt()
    {
        
    }

    // Update is called once per frame
	void Update () {
	
	}
}
