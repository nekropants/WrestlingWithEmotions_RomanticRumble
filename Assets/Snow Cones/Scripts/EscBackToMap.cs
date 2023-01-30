using UnityEngine;
using System.Collections;

public class EscBackToMap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape))
	    {
            if(SceneController.Instance.currentScene != SceneEnum.Map)
	            SceneController.ChangeScene(SceneEnum.Map);
            else
                Application.LoadLevel(Application.loadedLevel);
	    }
	}
}
