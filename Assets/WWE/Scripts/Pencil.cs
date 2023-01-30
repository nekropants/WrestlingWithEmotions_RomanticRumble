using UnityEngine;
using System.Collections;
using WWE;

public class Pencil : MonoBehaviour {


 public AnimationCurve xCurve;
 public AnimationCurve yCurve;
        
        float timer = 0;
        
        Vector3 offset;
        
		public float freq = 1;
		public float ampl = 1;
    public static Pencil instance;

    public bool down = false;


    public float spring = 10;
	// Use this for initialization
	void Awake () {
		instance = this;

	}
	
	
	float mouseDownTime = 0;
	// Update is called once per frame
	void Update () {
		
		
		down = (Input.GetKey(KeyCode.Mouse0));
		
			transform.position -= offset;
			
			bool doingStickerStuff = (  Stickers.instance.GetClosestSticker() != null);
		
		
		if(Input.GetKeyDown(KeyCode.Mouse0))
			mouseDownTime = Time.time;
		if(Input.GetKeyUp(KeyCode.Mouse0) && !doingStickerStuff && (Time.time - mouseDownTime) <0.2f)
			timer = 0;
			
		if(timer  < 1)
		{
			
			if(timer > 0.2f && timer <= 0.65f)
			{
				down = true;
			}
			timer +=Time.deltaTime*freq;
			timer = Mathf.Clamp01(timer);
			
			
			offset = xCurve.Evaluate(timer)*Vector3.right*ampl + yCurve.Evaluate(timer)*Vector3.up*ampl;
		
		}
		else
		{
			offset = Vector3.zero;
		}		
			transform.position += offset;
		


		
		if(Flex.selectedSticker != null )
			down = false;
	}
}
