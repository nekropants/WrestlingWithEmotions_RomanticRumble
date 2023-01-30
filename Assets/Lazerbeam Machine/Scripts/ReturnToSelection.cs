using UnityEngine;
using System.Collections;

public class ReturnToSelection : MonoBehaviour {

public static  ReturnToSelection instance;

	static bool prevState = false;
	static float delay = 0;
	float timeStamp =0;

	// Use this for initialization
	void Start () {

		//if(instance)
		//{
		//	DestroyImmediate(instance.gameObject);
		//	return;
		//}
		//DontDestroyOnLoad(gameObject);
		//instance = this;
		//prevState = Input.GetKey(KeyCode.F);
	
	}
	
	// Update is called once per frame
	void Update () {




		//bool newState = Input.GetKey(KeyCode.F);
		////if(newState != prevState)
		//{
			
		//	if(delay <=0)
		//	{
		//	if(Input.GetKeyDown(KeyCode.F))
		//	{
		//		print(newState);
		//		print(prevState);
		//		prevState = newState;
		//		delay = 5;
		//		Application.LoadLevel(0);
		//		timeStamp = Time.realtimeSinceStartup;
		//	}
		//	}
		//	else
		//	{
		//		delay -= Time.deltaTime;
		//	}
		//}
	}
}
