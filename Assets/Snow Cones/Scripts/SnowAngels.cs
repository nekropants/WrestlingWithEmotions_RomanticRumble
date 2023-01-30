using UnityEngine;
using System.Collections;

public class SnowAngels : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    private float timer = 4f;
	
	// Update is called once per frame
	void Update ()
	{

	    timer -= Time.deltaTime;
        if(timer < 0)
            SceneController.ChangeScene(SceneEnum.Map);
	}
}
