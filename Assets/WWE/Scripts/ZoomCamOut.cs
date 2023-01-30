using UnityEngine;
using System.Collections;

public class ZoomCamOut : MonoBehaviour
{
    private Camera cam;
    public float speed = 1;
	// Use this for initialization
	void Start ()
	{
	    cam = GetComponent<Camera>();

	}
	
	// Update is called once per frame
	void Update ()
	{
	    cam.orthographicSize += Time.deltaTime*speed;
	}
}
