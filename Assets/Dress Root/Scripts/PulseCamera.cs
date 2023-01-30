using UnityEngine;
using System.Collections;
namespace Dance { 
 public class PulseCamera : MonoBehaviour {

	Camera cam;
	public	int freq = 100;
	public float amp = 1;

	float offset = 0;

	float timer = 0;

    public static PulseCamera instance;
    public Shake shake;
	// Use this for initialization
	void Start ()
	{
	    instance = this;
		cam = GetComponent<Camera>();
	    shake = GetComponent<Shake>();
	}
	
	// Update is called once per frame
	void Update () {
		cam.orthographicSize -= offset;

		offset = Mathf.Sin(Time.time*(1/120f)*freq  )*amp;

		cam.orthographicSize += offset;


	
		
		timer += Time.deltaTime;
		while(timer > 1/3f)
		{
			timer -= 1/3f;
			float h = 0;
			float s = 0;
			float v = 0;
			Color.RGBToHSV(cam.backgroundColor, out h, out s , out v);
			h += 0.1f;
			cam.backgroundColor = Color.HSVToRGB(h, s ,v);
		}
	}
}

}