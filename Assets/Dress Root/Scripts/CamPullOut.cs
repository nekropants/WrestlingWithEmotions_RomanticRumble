using UnityEngine;
using System.Collections;

namespace Dance { 
 public class CamPullOut : MonoBehaviour {


	public Vector3 targetPos;
	public Quaternion startRot;
	 Vector3 startPos;

	public float targetSize ;
	float startSize ;

	public AnimationCurve curve;

	public float lerp = 0;
	public float speed =1;
	Camera cam;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		cam = GetComponent<Camera>();
		startSize = cam.orthographicSize;
		startRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

		lerp += Time.deltaTime* speed;
		lerp = Mathf.Clamp01(lerp);


		float l = curve.Evaluate(lerp);
		cam.orthographicSize = Mathf.Lerp(startSize, targetSize, l);
		transform.position = Vector3.Lerp(startPos, targetPos, l);
		transform.rotation = Quaternion.Lerp(startRot, Quaternion.identity, l);

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}

}