using UnityEngine;
using System.Collections;

namespace Dance { 
 public class ZoomInOnHead : MonoBehaviour
{

    public Transform head;
    private Vector3 startPos;

    public float lerp = 0;

    public float zoomSize;
    private float startSize;
    private Camera cam;
    private CameraMovement camMovement;
    private PulseCamera pulse;

    public Transform uiZoom;

    public bool lerpIn = false;

    private Vector3 offset;
    private Vector3 defaultScale;
    // Use this for initialization
    void Start ()
	{

	    cam = GetComponent<Camera>();
        pulse = GetComponent<PulseCamera>();
        camMovement = GetComponent<CameraMovement>();

        startSize = cam.orthographicSize;
	    startPos = transform.position;

        defaultScale = uiZoom.transform.localScale;

	}
	
	// Update is called once per frame
	void Update ()
	{

	    if (lerpIn)
	        lerp += Time.deltaTime;
	    else
	        lerp -= Time.deltaTime;


        lerp = Mathf.Clamp01(lerp);

	    Vector3 target = head.transform.position + Vector3.up;
	    target.z = startPos.z;

	    transform.position = Vector3.Lerp(startPos, target, lerp);

	    cam.orthographicSize = Mathf.Lerp(startSize, zoomSize, lerp);

	    uiZoom.transform.localScale = defaultScale * (1 + lerp*2);

	    camMovement.enabled = lerp == 0;
        pulse.enabled = lerp == 0;

    }
}

}