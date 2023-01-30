using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Dance { 
 public class ChangeScene : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene("Dance Floor");
        if (Input.GetKeyDown(KeyCode.Alpha2))
	        SceneManager.LoadScene("Bar");
	}
}

}