using UnityEngine;
using System.Collections;

public class DrinkingShakeController : MonoBehaviour {



    private float timer = 0;
	// Update is called once per frame
	void Update ()
	{

	    timer += Time.deltaTime;
        if (timer > 4)
        SceneController.ChangeScene(SceneEnum.Map);
	
	}
}
